using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EIMS.Migrations
{
    public partial class ExamEnrollments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExaminationStudent_Examinations_ExaminationId",
                table: "ExaminationStudent");

            migrationBuilder.DropForeignKey(
                name: "FK_ExaminationStudent_Students_StudentId",
                table: "ExaminationStudent");

            migrationBuilder.DropForeignKey(
                name: "FK_OfferedStudentExamSubject_Examinations_ExaminationId",
                table: "OfferedStudentExamSubject");

            migrationBuilder.DropForeignKey(
                name: "FK_OfferedStudentExamSubject_Students_StudentId",
                table: "OfferedStudentExamSubject");

            migrationBuilder.DropForeignKey(
                name: "FK_OfferedStudentExamSubject_Subjects_SubjectId",
                table: "OfferedStudentExamSubject");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OfferedStudentExamSubject",
                table: "OfferedStudentExamSubject");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExaminationStudent",
                table: "ExaminationStudent");

            migrationBuilder.RenameTable(
                name: "OfferedStudentExamSubject",
                newName: "OfferedStudentExamSubjects");

            migrationBuilder.RenameTable(
                name: "ExaminationStudent",
                newName: "ExaminationStudents");

            migrationBuilder.RenameIndex(
                name: "IX_OfferedStudentExamSubject_SubjectId",
                table: "OfferedStudentExamSubjects",
                newName: "IX_OfferedStudentExamSubjects_SubjectId");

            migrationBuilder.RenameIndex(
                name: "IX_OfferedStudentExamSubject_StudentId",
                table: "OfferedStudentExamSubjects",
                newName: "IX_OfferedStudentExamSubjects_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_OfferedStudentExamSubject_ExaminationId",
                table: "OfferedStudentExamSubjects",
                newName: "IX_OfferedStudentExamSubjects_ExaminationId");

            migrationBuilder.RenameIndex(
                name: "IX_ExaminationStudent_StudentId",
                table: "ExaminationStudents",
                newName: "IX_ExaminationStudents_StudentId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Teachers",
                nullable: false,
                defaultValue: new DateTime(2021, 7, 30, 14, 33, 9, 326, DateTimeKind.Local).AddTicks(6448),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 7, 30, 13, 43, 31, 162, DateTimeKind.Local).AddTicks(3738));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Examinations",
                nullable: false,
                defaultValue: new DateTime(2021, 7, 30, 14, 33, 9, 331, DateTimeKind.Local).AddTicks(462),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 7, 30, 13, 43, 31, 170, DateTimeKind.Local).AddTicks(8439));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Examinations",
                nullable: false,
                defaultValue: new DateTime(2021, 7, 30, 14, 33, 9, 331, DateTimeKind.Local).AddTicks(6198),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 7, 30, 13, 43, 31, 171, DateTimeKind.Local).AddTicks(4862));

            migrationBuilder.AddPrimaryKey(
                name: "PK_OfferedStudentExamSubjects",
                table: "OfferedStudentExamSubjects",
                columns: new[] { "ExamId", "StudentId", "SubjectId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExaminationStudents",
                table: "ExaminationStudents",
                columns: new[] { "ExaminationId", "StudentId" });

            migrationBuilder.CreateTable(
                name: "ExamEnrollments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExamId = table.Column<int>(nullable: false),
                    ExaminationId = table.Column<int>(nullable: true),
                    EnrollmentStatus = table.Column<int>(nullable: false),
                    SchoolId = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2021, 7, 30, 13, 33, 9, 334, DateTimeKind.Utc).AddTicks(9242)),
                    UpdatedAt = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2021, 7, 30, 13, 33, 9, 335, DateTimeKind.Utc).AddTicks(5653))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamEnrollments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExamEnrollments_Examinations_ExaminationId",
                        column: x => x.ExaminationId,
                        principalTable: "Examinations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExamEnrollments_Schools_SchoolId",
                        column: x => x.SchoolId,
                        principalTable: "Schools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExamEnrollments_ExaminationId",
                table: "ExamEnrollments",
                column: "ExaminationId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamEnrollments_SchoolId",
                table: "ExamEnrollments",
                column: "SchoolId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExaminationStudents_Examinations_ExaminationId",
                table: "ExaminationStudents",
                column: "ExaminationId",
                principalTable: "Examinations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExaminationStudents_Students_StudentId",
                table: "ExaminationStudents",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OfferedStudentExamSubjects_Examinations_ExaminationId",
                table: "OfferedStudentExamSubjects",
                column: "ExaminationId",
                principalTable: "Examinations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OfferedStudentExamSubjects_Students_StudentId",
                table: "OfferedStudentExamSubjects",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OfferedStudentExamSubjects_Subjects_SubjectId",
                table: "OfferedStudentExamSubjects",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExaminationStudents_Examinations_ExaminationId",
                table: "ExaminationStudents");

            migrationBuilder.DropForeignKey(
                name: "FK_ExaminationStudents_Students_StudentId",
                table: "ExaminationStudents");

            migrationBuilder.DropForeignKey(
                name: "FK_OfferedStudentExamSubjects_Examinations_ExaminationId",
                table: "OfferedStudentExamSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_OfferedStudentExamSubjects_Students_StudentId",
                table: "OfferedStudentExamSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_OfferedStudentExamSubjects_Subjects_SubjectId",
                table: "OfferedStudentExamSubjects");

            migrationBuilder.DropTable(
                name: "ExamEnrollments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OfferedStudentExamSubjects",
                table: "OfferedStudentExamSubjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExaminationStudents",
                table: "ExaminationStudents");

            migrationBuilder.RenameTable(
                name: "OfferedStudentExamSubjects",
                newName: "OfferedStudentExamSubject");

            migrationBuilder.RenameTable(
                name: "ExaminationStudents",
                newName: "ExaminationStudent");

            migrationBuilder.RenameIndex(
                name: "IX_OfferedStudentExamSubjects_SubjectId",
                table: "OfferedStudentExamSubject",
                newName: "IX_OfferedStudentExamSubject_SubjectId");

            migrationBuilder.RenameIndex(
                name: "IX_OfferedStudentExamSubjects_StudentId",
                table: "OfferedStudentExamSubject",
                newName: "IX_OfferedStudentExamSubject_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_OfferedStudentExamSubjects_ExaminationId",
                table: "OfferedStudentExamSubject",
                newName: "IX_OfferedStudentExamSubject_ExaminationId");

            migrationBuilder.RenameIndex(
                name: "IX_ExaminationStudents_StudentId",
                table: "ExaminationStudent",
                newName: "IX_ExaminationStudent_StudentId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Teachers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 7, 30, 13, 43, 31, 162, DateTimeKind.Local).AddTicks(3738),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 7, 30, 14, 33, 9, 326, DateTimeKind.Local).AddTicks(6448));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Examinations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 7, 30, 13, 43, 31, 170, DateTimeKind.Local).AddTicks(8439),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 7, 30, 14, 33, 9, 331, DateTimeKind.Local).AddTicks(462));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Examinations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 7, 30, 13, 43, 31, 171, DateTimeKind.Local).AddTicks(4862),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 7, 30, 14, 33, 9, 331, DateTimeKind.Local).AddTicks(6198));

            migrationBuilder.AddPrimaryKey(
                name: "PK_OfferedStudentExamSubject",
                table: "OfferedStudentExamSubject",
                columns: new[] { "ExamId", "StudentId", "SubjectId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExaminationStudent",
                table: "ExaminationStudent",
                columns: new[] { "ExaminationId", "StudentId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ExaminationStudent_Examinations_ExaminationId",
                table: "ExaminationStudent",
                column: "ExaminationId",
                principalTable: "Examinations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExaminationStudent_Students_StudentId",
                table: "ExaminationStudent",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OfferedStudentExamSubject_Examinations_ExaminationId",
                table: "OfferedStudentExamSubject",
                column: "ExaminationId",
                principalTable: "Examinations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OfferedStudentExamSubject_Students_StudentId",
                table: "OfferedStudentExamSubject",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OfferedStudentExamSubject_Subjects_SubjectId",
                table: "OfferedStudentExamSubject",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
