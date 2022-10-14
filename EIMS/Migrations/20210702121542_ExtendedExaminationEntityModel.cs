using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EIMS.Migrations
{
    public partial class ExtendedExaminationEntityModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Teachers",
                nullable: false,
                defaultValue: new DateTime(2021, 7, 2, 13, 15, 38, 715, DateTimeKind.Local).AddTicks(5900),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 6, 29, 14, 6, 0, 310, DateTimeKind.Local).AddTicks(4785));

            migrationBuilder.AlterColumn<decimal>(
                name: "ExamFee",
                table: "Examinations",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ExamYear",
                table: "Examinations",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExamYear",
                table: "Examinations");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Teachers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 6, 29, 14, 6, 0, 310, DateTimeKind.Local).AddTicks(4785),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 7, 2, 13, 15, 38, 715, DateTimeKind.Local).AddTicks(5900));

            migrationBuilder.AlterColumn<int>(
                name: "ExamFee",
                table: "Examinations",
                type: "int",
                nullable: true,
                oldClrType: typeof(decimal),
                oldNullable: true);
        }
    }
}
