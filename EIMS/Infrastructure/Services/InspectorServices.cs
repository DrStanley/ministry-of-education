using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EIMS.Domain.Entities;
using EIMS.Domain.Entities.Enum;
using EIMS.Domain.Identity;
using EIMS.Domain.Interfaces;
using EIMS.Infrastructure.Interfaces;
using EIMS.Models;

namespace EIMS.Infrastructure.Services
{

	public class InspectorServices : IInspectorServices
	{
		private readonly IMapper _mapper;
		private readonly IInspectorRepo _inspectorRepo;
		private readonly IAccountServices _accountServices;
		private readonly ILocalGovernmentRepo _localGovernmentRepo;
		private readonly IRoleServices _roleServices;
		private readonly IInspectorLoggerRepo _loggerRepo;


		public InspectorServices(IMapper mapper, 
			IInspectorRepo inspectorRepo, 
			IAccountServices accountServices,
			IRoleServices roleServices, ILocalGovernmentRepo localGovernmentRepo, IInspectorLoggerRepo loggerRepo)
		{
			_mapper = mapper;
			_inspectorRepo = inspectorRepo;
			_accountServices = accountServices;
			_roleServices = roleServices;
			_localGovernmentRepo = localGovernmentRepo;
			_loggerRepo = loggerRepo;
		}

		public async Task<string> CreateInspector(CreateInspectorViewModel inspectorViewModel)
		{
			var user = _mapper.Map<AppUser>(inspectorViewModel);
			user.UserName = inspectorViewModel.Email;
			var errorMessage = "";

			if (!IsLgaAssigned(inspectorViewModel.LocalGovernmentId))
			{
				var transaction =  _inspectorRepo.GetDbContextTransaction();

				try
				{ 
					var result = await _accountServices.CreateUser(user);
					if (result.Succeeded)
					{
						//Assign Inspector role to user
						var role = await _roleServices.FindRoleByNameAsync("Inspector");
						if (role != null)
						{
							var res = await _roleServices.AssignRoleAsync(user, role.Id);

							if (res.Succeeded)
							{

								var inspector = _mapper.Map<Inspector>(inspectorViewModel);
								inspector.Status = Status.Active;
								inspector.UserId = user.Id;
								inspector.CreatedAt = DateTime.Now;
								inspector.LocalGovernmentId = inspector.LocalGovernmentId == 0 ? null : inspector.LocalGovernmentId;
								_inspectorRepo.Insert(inspector);
								var success = _inspectorRepo.Save();
								if (success > 0)
								{
									await transaction.CommitAsync();
									return "Success";
								}
								await transaction.RollbackAsync();
							}
							else
							{
								await transaction.RollbackAsync();
								return res.Errors.Aggregate(errorMessage,
									(current, error) => current + $"{error.Description} \n");
							}

						}
					}
					await transaction.RollbackAsync();
					return result.Errors.Aggregate(errorMessage,
						(current, error) => current + $"{error.Description} \n");
				}
				catch (Exception e)
				{
					await transaction.RollbackAsync();
					return e.Message;
				}
			}
			return "Local Government has an Inspector already";
		}
	
		public async Task<string> UpdateInspector(CreateInspectorViewModel inspectorViewModel)
		{

			var errorMessage = "";
			try
			{
				var res = await _accountServices.FindUser(inspectorViewModel.Email);
				res.Name = inspectorViewModel.Name;
				res.PhoneNumber = inspectorViewModel.PhoneNumber;
				res.Title = inspectorViewModel.Title;
				var result = await _accountServices.UpdateUser(res);
				if (result.Succeeded)
				{
					return "Success";
				}

				return result.Errors.Aggregate(errorMessage, (current, error) => current + $"{error.Description} \n");
			}
			catch (Exception e)
			{
				return e.Message;
			}
		}

		public IEnumerable<InspectorViewModel> GetInspectors()
		{
			return _mapper.Map<IEnumerable<InspectorViewModel>>(_inspectorRepo.AllInclude(t => t.User,t => t.LocalGovernment));
		}

		public InspectorViewModel ChangeInspectorStatus(int id)
		{
			try
			{
				var inspector = _inspectorRepo.GetById(id);
				inspector.Status = inspector.Status == Status.Active ? Status.Inactive : Status.Active;
				_inspectorRepo.Update(inspector);
				_inspectorRepo.Save();
				return _mapper.Map<InspectorViewModel>(inspector);

			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				return null;
		    }
		}

		public void DeleteInspector(int id)
		{
			_inspectorRepo.DeleteInspector(id);
		}

		public  bool IsLgaAssigned(int localGovernmentId)
		{
			var i = _inspectorRepo.FindOne(i => i.LocalGovernmentId == localGovernmentId);
			if (i==null)
			{
				return false;
			}
			return true;
		}

		public string AssignInspector(int id,int lgaId)
		{
			var transaction = _inspectorRepo.GetDbContextTransaction();

			try
			{
				if (!IsLgaAssigned(lgaId))
				{
					var inspector = _inspectorRepo.FindOneInclude(i=>i.Id==id,i=>i.User,i=>i.LocalGovernment);
					inspector.LocalGovernmentId = lgaId;
					_inspectorRepo.Update(inspector);
					var log = _mapper.Map<InspectorLog>(inspector);
					log.LogType = LogType.Assign;
					log.Details = inspector.User.Name + " was assigned to LGA: " + _localGovernmentRepo.GetById(lgaId).Name;
					log.CreatedAt = DateTime.Now;

					_loggerRepo.Insert(log);
					_loggerRepo.Save();
					transaction.Commit();
					return "Success";
				}

				return "This LGA is assigned to an Inspector";
			}
			catch (Exception e)
			{
				return e.Message;
			}
		}
		
		public string UnAssignInspector(int id)
		{
			var transaction = _loggerRepo.GetDbContextTransaction();

			try
			{
				var inspector = _inspectorRepo.FindOneInclude(i => i.Id == id, i => i.User, i => i.LocalGovernment);

				var log = _mapper.Map<InspectorLog>(inspector);
				log.LogType = LogType.UnAssign;
				log.Details = inspector.User.Name + " was unassigned from LGA: " + inspector.LocalGovernment.Name;
				log.CreatedAt = DateTime.Now;
				_loggerRepo.Insert(log);
				//_loggerRepo.Save();

				inspector.LocalGovernmentId = null;
				inspector.LocalGovernment = null;
				_inspectorRepo.Update(inspector);
				_inspectorRepo.Save();
				transaction.Commit();
				return "Success";
			}
			catch (Exception e)
			{
				transaction.Rollback();
				return e.Message;
			}
		}

		public string TransferInspector(TransferInspectorViewModel transferInspectorViewModel)
		{

			if (IsLgaAssigned(transferInspectorViewModel.LocalGovernmentIdTo))
			{
				var inspect = _inspectorRepo.FindOne(i =>
					i.LocalGovernmentId == transferInspectorViewModel.LocalGovernmentIdTo);
				UnAssignInspector(inspect.Id);
			}

			var transaction = _loggerRepo.GetDbContextTransaction();

			try
			{
				
				var inspector = _inspectorRepo.FindOneInclude(i => i.Id == transferInspectorViewModel.Id, i => i.User, i => i.LocalGovernment);


				var log = _mapper.Map<InspectorLog>(transferInspectorViewModel);
				log.LogType = LogType.Transfer;

				log.Details = inspector.User.Name + " was transferred from LGA: " + inspector.LocalGovernment.Name +
				              ", to LGA: " +_localGovernmentRepo.GetById(transferInspectorViewModel.LocalGovernmentIdTo).Name;
				
				log.CreatedAt = DateTime.Now;

				_loggerRepo.Insert(log);
				//_loggerRepo.Save();

				inspector.LocalGovernmentId = transferInspectorViewModel.LocalGovernmentIdTo;
				_inspectorRepo.Update(inspector);
				_inspectorRepo.Save();
				transaction.Commit();
				return "Success";
			}
			catch (Exception e)
			{
				transaction.Rollback();
				return e.Message;
			}
		}


	}
}