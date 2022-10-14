using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EIMS.Migrations
{
    public partial class ClassType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Teachers",
                nullable: false,
                defaultValue: new DateTime(2021, 7, 3, 16, 31, 24, 851, DateTimeKind.Local).AddTicks(7215),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 6, 29, 14, 6, 0, 310, DateTimeKind.Local).AddTicks(4785));

            migrationBuilder.AddColumn<int>(
                name: "ClassType",
                table: "Classes",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClassType",
                table: "Classes");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Teachers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 6, 29, 14, 6, 0, 310, DateTimeKind.Local).AddTicks(4785),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 7, 3, 16, 31, 24, 851, DateTimeKind.Local).AddTicks(7215));
        }
    }
}
