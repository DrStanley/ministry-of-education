using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EIMS.Migrations
{
    public partial class LocalGovernmentUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "LocalGovernments",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "LocalGovernments",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "LocalGovernments",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Teachers",
                nullable: false,
                defaultValue: new DateTime(2021, 7, 20, 17, 19, 32, 122, DateTimeKind.Local).AddTicks(2349),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 7, 9, 9, 15, 31, 278, DateTimeKind.Local).AddTicks(9272));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Examinations",
                nullable: false,
                defaultValue: new DateTime(2021, 7, 20, 17, 19, 32, 124, DateTimeKind.Local).AddTicks(4481),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 7, 9, 9, 15, 31, 284, DateTimeKind.Local).AddTicks(3597));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Examinations",
                nullable: false,
                defaultValue: new DateTime(2021, 7, 20, 17, 19, 32, 124, DateTimeKind.Local).AddTicks(6493),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 7, 9, 9, 15, 31, 284, DateTimeKind.Local).AddTicks(8463));

            migrationBuilder.UpdateData(
                table: "LocalGovernments",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Ado-Ekiti");

            migrationBuilder.UpdateData(
                table: "LocalGovernments",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Ikere");

            migrationBuilder.UpdateData(
                table: "LocalGovernments",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Oye");

            migrationBuilder.UpdateData(
                table: "LocalGovernments",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Aiyekire (Gbonyin)");

            migrationBuilder.UpdateData(
                table: "LocalGovernments",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "Efon");

            migrationBuilder.UpdateData(
                table: "LocalGovernments",
                keyColumn: "Id",
                keyValue: 6,
                column: "Name",
                value: "Ekiti East");

            migrationBuilder.UpdateData(
                table: "LocalGovernments",
                keyColumn: "Id",
                keyValue: 7,
                column: "Name",
                value: "Ekiti South-West");

            migrationBuilder.UpdateData(
                table: "LocalGovernments",
                keyColumn: "Id",
                keyValue: 8,
                column: "Name",
                value: "Ekiti West");

            migrationBuilder.UpdateData(
                table: "LocalGovernments",
                keyColumn: "Id",
                keyValue: 9,
                column: "Name",
                value: "Emure");

            migrationBuilder.UpdateData(
                table: "LocalGovernments",
                keyColumn: "Id",
                keyValue: 10,
                column: "Name",
                value: "Ido-Osi");

            migrationBuilder.UpdateData(
                table: "LocalGovernments",
                keyColumn: "Id",
                keyValue: 11,
                column: "Name",
                value: "Ijero");

            migrationBuilder.UpdateData(
                table: "LocalGovernments",
                keyColumn: "Id",
                keyValue: 12,
                column: "Name",
                value: "Ikole");

            migrationBuilder.UpdateData(
                table: "LocalGovernments",
                keyColumn: "Id",
                keyValue: 13,
                column: "Name",
                value: "Ilejemeje");

            migrationBuilder.UpdateData(
                table: "LocalGovernments",
                keyColumn: "Id",
                keyValue: 14,
                column: "Name",
                value: "Irepodun/Ifelodun");

            migrationBuilder.UpdateData(
                table: "LocalGovernments",
                keyColumn: "Id",
                keyValue: 15,
                column: "Name",
                value: "Ise/Orun");

            migrationBuilder.UpdateData(
                table: "LocalGovernments",
                keyColumn: "Id",
                keyValue: 16,
                column: "Name",
                value: "Moba");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Teachers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 7, 9, 9, 15, 31, 278, DateTimeKind.Local).AddTicks(9272),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 7, 20, 17, 19, 32, 122, DateTimeKind.Local).AddTicks(2349));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Examinations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 7, 9, 9, 15, 31, 284, DateTimeKind.Local).AddTicks(3597),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 7, 20, 17, 19, 32, 124, DateTimeKind.Local).AddTicks(4481));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Examinations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 7, 9, 9, 15, 31, 284, DateTimeKind.Local).AddTicks(8463),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 7, 20, 17, 19, 32, 124, DateTimeKind.Local).AddTicks(6493));

            migrationBuilder.UpdateData(
                table: "LocalGovernments",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Akoko Edo");

            migrationBuilder.UpdateData(
                table: "LocalGovernments",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Egor");

            migrationBuilder.UpdateData(
                table: "LocalGovernments",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Esan Central");

            migrationBuilder.UpdateData(
                table: "LocalGovernments",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Esan North-East");

            migrationBuilder.UpdateData(
                table: "LocalGovernments",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "Esan South-East");

            migrationBuilder.UpdateData(
                table: "LocalGovernments",
                keyColumn: "Id",
                keyValue: 6,
                column: "Name",
                value: "Esan West");

            migrationBuilder.UpdateData(
                table: "LocalGovernments",
                keyColumn: "Id",
                keyValue: 7,
                column: "Name",
                value: "Etsako Central");

            migrationBuilder.UpdateData(
                table: "LocalGovernments",
                keyColumn: "Id",
                keyValue: 8,
                column: "Name",
                value: "Etsako East");

            migrationBuilder.UpdateData(
                table: "LocalGovernments",
                keyColumn: "Id",
                keyValue: 9,
                column: "Name",
                value: "Etsako West");

            migrationBuilder.UpdateData(
                table: "LocalGovernments",
                keyColumn: "Id",
                keyValue: 10,
                column: "Name",
                value: "Etsako West");

            migrationBuilder.UpdateData(
                table: "LocalGovernments",
                keyColumn: "Id",
                keyValue: 11,
                column: "Name",
                value: "Igueben");

            migrationBuilder.UpdateData(
                table: "LocalGovernments",
                keyColumn: "Id",
                keyValue: 12,
                column: "Name",
                value: "Ikpoba Okha");

            migrationBuilder.UpdateData(
                table: "LocalGovernments",
                keyColumn: "Id",
                keyValue: 13,
                column: "Name",
                value: "Oredo");

            migrationBuilder.UpdateData(
                table: "LocalGovernments",
                keyColumn: "Id",
                keyValue: 14,
                column: "Name",
                value: "Orhionmwon");

            migrationBuilder.UpdateData(
                table: "LocalGovernments",
                keyColumn: "Id",
                keyValue: 15,
                column: "Name",
                value: "Ovia North East");

            migrationBuilder.UpdateData(
                table: "LocalGovernments",
                keyColumn: "Id",
                keyValue: 16,
                column: "Name",
                value: "Ovia South East");

            migrationBuilder.InsertData(
                table: "LocalGovernments",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 18, "Owan West" },
                    { 19, "Uhunmwonde" },
                    { 17, "Owan East" }
                });
        }
    }
}
