using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EIMS.Domain.Entities;
using EIMS.Domain.Entities.Enum;
using EIMS.Domain.Identity;
using EIMS.Domain.Interfaces;
using EIMS.Infrastructure.Interfaces;
using EIMS.Models;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;

namespace EIMS.Infrastructure.Services
{
	public class SchoolService : ISchoolService

	{
		private readonly IMapper _mapper;
		private readonly ILocalGovernmentRepo _lgaRepo;
		private readonly ISchoolRepo _schoolRepo;
		private readonly IRoleServices _roleServices;
		private readonly IProfileRepo _profileRepo;
		private readonly IAccountServices _accountServices;

		public SchoolService(ISchoolRepo schoolRepo,
			IRoleServices roleServices,
			IProfileRepo profileRepo,
			IAccountServices accountServices,
			IMapper mapper,
			ILocalGovernmentRepo localGovernmentRepo)
		{
			_accountServices = accountServices;
            _profileRepo = profileRepo;
			_roleServices = roleServices;
			_schoolRepo = schoolRepo;
			_mapper = mapper;
			_lgaRepo = localGovernmentRepo;
		}

		public async Task<string> AddNewSchool(CreateSchoolViewModel schoolViewModel)
		{
			var user = _mapper.Map<AppUser>(schoolViewModel);	
			var errorMessage = "";
			var transaction = await _schoolRepo.GetDbContext().Database.BeginTransactionAsync();

			try
			{
				var result = await _accountServices.CreateUser(user);

				if (result.Succeeded)
				{
					var res = _schoolRepo.CreateSchool(_mapper.Map<School>(schoolViewModel), user.Id);
					if (res > 0)
					{
						var role = await _roleServices.FindRoleByNameAsync("SchoolAdmin");
						if (role != null)
						{
							var roleResult = await _roleServices.AssignRoleAsync(user, role.Id);
							var profileResult = await _profileRepo.CreateProfile(user.Id);
							if (!(roleResult.Succeeded && profileResult.Item1 > 0))
							{
								await transaction.RollbackAsync();
								foreach (var error in roleResult.Errors)
								{
									errorMessage += $"{error.Description} \n";
								}
								return errorMessage;
							}
						}

						await transaction.CommitAsync();
						return"Success";
					}

					await transaction.RollbackAsync();

					return "Error occurred while creating";
				}
				foreach (var error in result.Errors)
				{
					errorMessage += $"{error.Description} \n";
				}
			}
			catch (Exception e)
			{
				await transaction.RollbackAsync();

				Console.WriteLine(e);
				throw;
			}

			
			return errorMessage;
		}


		public School GetSchool(int schoolId)
		{
			return _schoolRepo.FindSchool(schoolId);
		}

		public string EditSchoolDetails(EditSchoolViewModel schoolViewModel)
		{
			var schoolData = _mapper.Map<School>(schoolViewModel);
			try
			{
				_schoolRepo.UpdateSchool(schoolData);
				return"Success";
			}
			catch (Exception e)
			{
				return e.Message;
			}

		}

		public IEnumerable<SchoolViewModel> GetSchools(string query = null)
		{
			var schools = _mapper.Map<IEnumerable<SchoolViewModel>>(_schoolRepo.GetAllSchoolsAsync().Result).OrderBy(s => s.SchoolName);
			return query switch
			{
				"public" => schools.Where(s => s.Type == SchoolType.PUBLIC),
				"private" => schools.Where(s => s.Type == SchoolType.PRIVATE),
				"active" => schools.Where(s => s.Status == Status.Active),
				"inactive" => schools.Where(s => s.Status == Status.Inactive),
                _ => schools
			};
		}

		public async Task<string> UploadSchoolAsync(IFormFile formFile)
		{
			try
			{
				var error = "";

				var (createSchoolViewModels, item2) = await ExtractSchools(formFile);
				if (item2 == "Success")
				{
					if (createSchoolViewModels.Count > 0)
					{
						var count = 0;
						foreach (var school in createSchoolViewModels)
						{
							count++;
							var res = await AddNewSchool(school);
							if (res != "Success")
							{
								error += $"\n{count}. "+ res + " school name: " + school.SchoolName + "\n";
							}
						}
						return error != "" ? error : "Success";
					}
					return "No data found";
				}
				return item2;
			}
			catch (Exception e)
			{
				return e.Message;
			}

		}

        public SchoolViewModel ChangeStatus(int id)
        {
            var school = _schoolRepo.FindSchool(id);
            if (school.Status == Status.Active)
            {
                school.Status = Status.Inactive;
            }
            else
            {
                school.Status = Status.Active;
            }

            _schoolRepo.UpdateSchool(school);

            return _mapper.Map<SchoolViewModel>(school);
        }

        public void DeleteSchoolAsync(int id)
		{
			try
			{
				var school = _schoolRepo.FindSchool(id);
				/*				 await _accountServices.DeleteUser(school.UserId);
				*/
				_schoolRepo.DeleteSchool(school.Id);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}

		}
		private async Task<(List<CreateSchoolViewModel>,string)> ExtractSchools(IFormFile file)
		{
			var schoolList = new List<CreateSchoolViewModel>();
			var allLga = _lgaRepo.GetAll().ToList();
			try
			{
				var stream = new MemoryStream();
				await file.CopyToAsync(stream);

				var package = new ExcelPackage(stream);
				ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
				var rowCount = worksheet.Dimension.Rows;
				for (int row = 2; row <= rowCount; row++)
				{
					schoolList.Add(new CreateSchoolViewModel
					{
						SchoolName = worksheet.Cells[row, 1].Value.ToString(),
						SchoolCode = worksheet.Cells[row, 2].Value.ToString(),
						Email = worksheet.Cells[row, 3].Value.ToString(),
						PhoneNumber = worksheet.Cells[row, 4].Value.ToString(),
						Address = worksheet.Cells[row, 5].Value.ToString(),
						LocalGovernmentId = allLga.First(c => c.Name.ToUpper() == worksheet.Cells[row, 6].Value.ToString().ToUpper()).Id,
						IncorporationDate = DateTime.Parse(worksheet.Cells[row, 7].Value.ToString()),
						Title = worksheet.Cells[row, 8].Value.ToString(),
						PrincipalName = worksheet.Cells[row, 9].Value.ToString(),

						SchoolCategory = (SchoolCategory)Enum.Parse(
							typeof(SchoolCategory), worksheet.Cells[row, 10].Value.ToString().ToUpper().Replace(" ", "")),
						SchoolType = (SchoolType)Enum.Parse(
							typeof(SchoolType), worksheet.Cells[row, 11].Value.ToString().ToUpper().Replace(" ", ""))
					});
				}
				return (schoolList,"Success");

			}
			catch (Exception e)
			{
				return (schoolList, e.Message);

			}

		}
	}


}
