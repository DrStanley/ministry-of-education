using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EIMS.Migrations
{
    public partial class StudentExamEnrollment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Teachers",
                nullable: false,
                defaultValue: new DateTime(2021, 7, 30, 13, 43, 31, 162, DateTimeKind.Local).AddTicks(3738),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 7, 28, 14, 51, 48, 335, DateTimeKind.Local).AddTicks(516));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Examinations",
                nullable: false,
                defaultValue: new DateTime(2021, 7, 30, 13, 43, 31, 170, DateTimeKind.Local).AddTicks(8439),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 7, 28, 14, 51, 48, 339, DateTimeKind.Local).AddTicks(5934));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Examinations",
                nullable: false,
                defaultValue: new DateTime(2021, 7, 30, 13, 43, 31, 171, DateTimeKind.Local).AddTicks(4862),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 7, 28, 14, 51, 48, 340, DateTimeKind.Local).AddTicks(794));

            migrationBuilder.CreateTable(
                name: "ExaminationStudent",
                columns: table => new
                {
                    StudentId = table.Column<int>(nullable: false),
                    ExaminationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExaminationStudent", x => new { x.ExaminationId, x.StudentId });
                    table.ForeignKey(
                        name: "FK_ExaminationStudent_Examinations_ExaminationId",
                        column: x => x.ExaminationId,
                        principalTable: "Examinations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExaminationStudent_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OfferedStudentExamSubject",
                columns: table => new
                {
                    StudentId = table.Column<int>(nullable: false),
                    ExamId = table.Column<int>(nullable: false),
                    SubjectId = table.Column<int>(nullable: false),
                    ExaminationId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferedStudentExamSubject", x => new { x.ExamId, x.StudentId, x.SubjectId });
                    table.ForeignKey(
                        name: "FK_OfferedStudentExamSubject_Examinations_ExaminationId",
                        column: x => x.ExaminationId,
                        principalTable: "Examinations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OfferedStudentExamSubject_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OfferedStudentExamSubject_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExaminationStudent_StudentId",
                table: "ExaminationStudent",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_OfferedStudentExamSubject_ExaminationId",
                table: "OfferedStudentExamSubject",
                column: "ExaminationId");

            migrationBuilder.CreateIndex(
                name: "IX_OfferedStudentExamSubject_StudentId",
                table: "OfferedStudentExamSubject",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_OfferedStudentExamSubject_SubjectId",
                table: "OfferedStudentExamSubject",
                column: "SubjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExaminationStudent");

            migrationBuilder.DropTable(
                name: "OfferedStudentExamSubject");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Teachers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 7, 28, 14, 51, 48, 335, DateTimeKind.Local).AddTicks(516),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 7, 30, 13, 43, 31, 162, DateTimeKind.Local).AddTicks(3738));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Examinations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 7, 28, 14, 51, 48, 339, DateTimeKind.Local).AddTicks(5934),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 7, 30, 13, 43, 31, 170, DateTimeKind.Local).AddTicks(8439));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Examinations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 7, 28, 14, 51, 48, 340, DateTimeKind.Local).AddTicks(794),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 7, 30, 13, 43, 31, 171, DateTimeKind.Local).AddTicks(4862));
        }
    }
}
