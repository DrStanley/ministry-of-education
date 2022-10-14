using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EIMS.Migrations
{
    public partial class ChangedExaminationFeeColumnType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Teachers",
                nullable: false,
                defaultValue: new DateTime(2021, 7, 2, 14, 46, 49, 742, DateTimeKind.Local).AddTicks(123),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 7, 2, 13, 15, 38, 715, DateTimeKind.Local).AddTicks(5900));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Examinations",
                nullable: false,
                defaultValue: new DateTime(2021, 7, 2, 14, 46, 49, 746, DateTimeKind.Local).AddTicks(7516),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<decimal>(
                name: "ExamFee",
                table: "Examinations",
                type: "decimal(14, 2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Examinations",
                nullable: false,
                defaultValue: new DateTime(2021, 7, 2, 14, 46, 49, 747, DateTimeKind.Local).AddTicks(1437),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Teachers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 7, 2, 13, 15, 38, 715, DateTimeKind.Local).AddTicks(5900),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 7, 2, 14, 46, 49, 742, DateTimeKind.Local).AddTicks(123));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Examinations",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 7, 2, 14, 46, 49, 746, DateTimeKind.Local).AddTicks(7516));

            migrationBuilder.AlterColumn<decimal>(
                name: "ExamFee",
                table: "Examinations",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(14, 2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Examinations",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 7, 2, 14, 46, 49, 747, DateTimeKind.Local).AddTicks(1437));
        }
    }
}
