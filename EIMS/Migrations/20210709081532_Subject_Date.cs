using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EIMS.Migrations
{
    public partial class Subject_Date : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Teachers",
                nullable: false,
                defaultValue: new DateTime(2021, 7, 9, 9, 15, 31, 278, DateTimeKind.Local).AddTicks(9272),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 7, 7, 15, 57, 12, 924, DateTimeKind.Local).AddTicks(8861));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Subjects",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Examinations",
                nullable: false,
                defaultValue: new DateTime(2021, 7, 9, 9, 15, 31, 284, DateTimeKind.Local).AddTicks(3597),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 7, 7, 15, 57, 12, 930, DateTimeKind.Local).AddTicks(5112));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Examinations",
                nullable: false,
                defaultValue: new DateTime(2021, 7, 9, 9, 15, 31, 284, DateTimeKind.Local).AddTicks(8463),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 7, 7, 15, 57, 12, 930, DateTimeKind.Local).AddTicks(9966));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Subjects");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Teachers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 7, 7, 15, 57, 12, 924, DateTimeKind.Local).AddTicks(8861),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 7, 9, 9, 15, 31, 278, DateTimeKind.Local).AddTicks(9272));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Examinations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 7, 7, 15, 57, 12, 930, DateTimeKind.Local).AddTicks(5112),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 7, 9, 9, 15, 31, 284, DateTimeKind.Local).AddTicks(3597));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Examinations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 7, 7, 15, 57, 12, 930, DateTimeKind.Local).AddTicks(9966),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 7, 9, 9, 15, 31, 284, DateTimeKind.Local).AddTicks(8463));
        }
    }
}
