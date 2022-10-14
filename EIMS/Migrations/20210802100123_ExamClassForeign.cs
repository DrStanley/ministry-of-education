using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EIMS.Migrations
{
    public partial class ExamClassForeign : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Teachers",
                nullable: false,
                defaultValue: new DateTime(2021, 8, 2, 11, 1, 21, 46, DateTimeKind.Local).AddTicks(2322),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 7, 30, 14, 33, 9, 326, DateTimeKind.Local).AddTicks(6448));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Examinations",
                nullable: false,
                defaultValue: new DateTime(2021, 8, 2, 11, 1, 21, 60, DateTimeKind.Local).AddTicks(6754),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 7, 30, 14, 33, 9, 331, DateTimeKind.Local).AddTicks(462));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Examinations",
                nullable: false,
                defaultValue: new DateTime(2021, 8, 2, 11, 1, 21, 61, DateTimeKind.Local).AddTicks(7863),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 7, 30, 14, 33, 9, 331, DateTimeKind.Local).AddTicks(6198));

            migrationBuilder.AddColumn<int>(
                name: "ClassId",
                table: "Examinations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ExamClassId",
                table: "Examinations",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "ExamEnrollments",
                nullable: false,
                defaultValue: new DateTime(2021, 8, 2, 10, 1, 21, 69, DateTimeKind.Utc).AddTicks(6989),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 7, 30, 13, 33, 9, 335, DateTimeKind.Utc).AddTicks(5653));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ExamEnrollments",
                nullable: false,
                defaultValue: new DateTime(2021, 8, 2, 10, 1, 21, 69, DateTimeKind.Utc).AddTicks(401),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 7, 30, 13, 33, 9, 334, DateTimeKind.Utc).AddTicks(9242));

            migrationBuilder.CreateIndex(
                name: "IX_Examinations_ExamClassId",
                table: "Examinations",
                column: "ExamClassId");

            migrationBuilder.AddForeignKey(
                name: "FK_Examinations_Classes_ExamClassId",
                table: "Examinations",
                column: "ExamClassId",
                principalTable: "Classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Examinations_Classes_ExamClassId",
                table: "Examinations");

            migrationBuilder.DropIndex(
                name: "IX_Examinations_ExamClassId",
                table: "Examinations");

            migrationBuilder.DropColumn(
                name: "ClassId",
                table: "Examinations");

            migrationBuilder.DropColumn(
                name: "ExamClassId",
                table: "Examinations");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Teachers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 7, 30, 14, 33, 9, 326, DateTimeKind.Local).AddTicks(6448),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 8, 2, 11, 1, 21, 46, DateTimeKind.Local).AddTicks(2322));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Examinations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 7, 30, 14, 33, 9, 331, DateTimeKind.Local).AddTicks(462),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 8, 2, 11, 1, 21, 60, DateTimeKind.Local).AddTicks(6754));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Examinations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 7, 30, 14, 33, 9, 331, DateTimeKind.Local).AddTicks(6198),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 8, 2, 11, 1, 21, 61, DateTimeKind.Local).AddTicks(7863));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "ExamEnrollments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 7, 30, 13, 33, 9, 335, DateTimeKind.Utc).AddTicks(5653),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 8, 2, 10, 1, 21, 69, DateTimeKind.Utc).AddTicks(6989));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ExamEnrollments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 7, 30, 13, 33, 9, 334, DateTimeKind.Utc).AddTicks(9242),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 8, 2, 10, 1, 21, 69, DateTimeKind.Utc).AddTicks(401));
        }
    }
}
