using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EIMS.Migrations
{
    public partial class InspectorUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inspectors_LocalGovernments_LocalGovernmentId",
                table: "Inspectors");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Teachers",
                nullable: false,
                defaultValue: new DateTime(2021, 7, 28, 14, 51, 48, 335, DateTimeKind.Local).AddTicks(516),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 7, 24, 22, 35, 48, 310, DateTimeKind.Local).AddTicks(463));

            migrationBuilder.AlterColumn<int>(
                name: "LocalGovernmentId",
                table: "Inspectors",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Examinations",
                nullable: false,
                defaultValue: new DateTime(2021, 7, 28, 14, 51, 48, 339, DateTimeKind.Local).AddTicks(5934),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 7, 24, 22, 35, 48, 312, DateTimeKind.Local).AddTicks(3970));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Examinations",
                nullable: false,
                defaultValue: new DateTime(2021, 7, 28, 14, 51, 48, 340, DateTimeKind.Local).AddTicks(794),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 7, 24, 22, 35, 48, 312, DateTimeKind.Local).AddTicks(5840));

            migrationBuilder.AddForeignKey(
                name: "FK_Inspectors_LocalGovernments_LocalGovernmentId",
                table: "Inspectors",
                column: "LocalGovernmentId",
                principalTable: "LocalGovernments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inspectors_LocalGovernments_LocalGovernmentId",
                table: "Inspectors");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Teachers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 7, 24, 22, 35, 48, 310, DateTimeKind.Local).AddTicks(463),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 7, 28, 14, 51, 48, 335, DateTimeKind.Local).AddTicks(516));

            migrationBuilder.AlterColumn<int>(
                name: "LocalGovernmentId",
                table: "Inspectors",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Examinations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 7, 24, 22, 35, 48, 312, DateTimeKind.Local).AddTicks(3970),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 7, 28, 14, 51, 48, 339, DateTimeKind.Local).AddTicks(5934));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Examinations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 7, 24, 22, 35, 48, 312, DateTimeKind.Local).AddTicks(5840),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 7, 28, 14, 51, 48, 340, DateTimeKind.Local).AddTicks(794));

            migrationBuilder.AddForeignKey(
                name: "FK_Inspectors_LocalGovernments_LocalGovernmentId",
                table: "Inspectors",
                column: "LocalGovernmentId",
                principalTable: "LocalGovernments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
