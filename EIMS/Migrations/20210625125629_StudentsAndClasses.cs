using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EIMS.Migrations
{
    public partial class StudentsAndClasses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Teachers",
                nullable: false,
                defaultValue: new DateTime(2021, 6, 25, 13, 56, 27, 805, DateTimeKind.Local).AddTicks(5961),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 6, 25, 10, 46, 26, 983, DateTimeKind.Local).AddTicks(2515));

            migrationBuilder.AddColumn<int>(
                name: "ClassesId",
                table: "Students",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_ClassesId",
                table: "Students",
                column: "ClassesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Classes_ClassesId",
                table: "Students",
                column: "ClassesId",
                principalTable: "Classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Classes_ClassesId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_ClassesId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "ClassesId",
                table: "Students");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Teachers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 6, 25, 10, 46, 26, 983, DateTimeKind.Local).AddTicks(2515),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 6, 25, 13, 56, 27, 805, DateTimeKind.Local).AddTicks(5961));
        }
    }
}
