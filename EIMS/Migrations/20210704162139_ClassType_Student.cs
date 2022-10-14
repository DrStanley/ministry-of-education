using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EIMS.Migrations
{
    public partial class ClassType_Student : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Teachers",
                nullable: false,
                defaultValue: new DateTime(2021, 7, 4, 17, 21, 36, 343, DateTimeKind.Local).AddTicks(2357),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 7, 2, 14, 46, 49, 742, DateTimeKind.Local).AddTicks(123));

            migrationBuilder.AddColumn<int>(
                name: "ClassType",
                table: "Students",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Examinations",
                nullable: false,
                defaultValue: new DateTime(2021, 7, 4, 17, 21, 36, 348, DateTimeKind.Local).AddTicks(9068),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 7, 2, 14, 46, 49, 746, DateTimeKind.Local).AddTicks(7516));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Examinations",
                nullable: false,
                defaultValue: new DateTime(2021, 7, 4, 17, 21, 36, 349, DateTimeKind.Local).AddTicks(3987),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 7, 2, 14, 46, 49, 747, DateTimeKind.Local).AddTicks(1437));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClassType",
                table: "Students");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Teachers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 7, 2, 14, 46, 49, 742, DateTimeKind.Local).AddTicks(123),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 7, 4, 17, 21, 36, 343, DateTimeKind.Local).AddTicks(2357));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Examinations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 7, 2, 14, 46, 49, 746, DateTimeKind.Local).AddTicks(7516),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 7, 4, 17, 21, 36, 348, DateTimeKind.Local).AddTicks(9068));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Examinations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 7, 2, 14, 46, 49, 747, DateTimeKind.Local).AddTicks(1437),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 7, 4, 17, 21, 36, 349, DateTimeKind.Local).AddTicks(3987));
        }
    }
}
