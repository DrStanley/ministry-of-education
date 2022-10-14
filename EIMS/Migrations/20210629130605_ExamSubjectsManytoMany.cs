using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EIMS.Migrations
{
    public partial class ExamSubjectsManytoMany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Teachers",
                nullable: false,
                defaultValue: new DateTime(2021, 6, 29, 14, 6, 0, 310, DateTimeKind.Local).AddTicks(4785),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 6, 25, 13, 56, 27, 805, DateTimeKind.Local).AddTicks(5961));

            migrationBuilder.AddColumn<int>(
                name: "ExamFee",
                table: "Examinations",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ExamStatus",
                table: "Examinations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ExaminationsSubjects",
                columns: table => new
                {
                    ExaminationId = table.Column<int>(nullable: false),
                    SubjectId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExaminationsSubjects", x => new { x.ExaminationId, x.SubjectId });
                    table.ForeignKey(
                        name: "FK_ExaminationsSubjects_Examinations_ExaminationId",
                        column: x => x.ExaminationId,
                        principalTable: "Examinations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExaminationsSubjects_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExaminationsSubjects_SubjectId",
                table: "ExaminationsSubjects",
                column: "SubjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExaminationsSubjects");

            migrationBuilder.DropColumn(
                name: "ExamFee",
                table: "Examinations");

            migrationBuilder.DropColumn(
                name: "ExamStatus",
                table: "Examinations");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Teachers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 6, 25, 13, 56, 27, 805, DateTimeKind.Local).AddTicks(5961),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 6, 29, 14, 6, 0, 310, DateTimeKind.Local).AddTicks(4785));
        }
    }
}
