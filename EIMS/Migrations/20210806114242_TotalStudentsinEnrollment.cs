using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EIMS.Migrations
{
    public partial class TotalStudentsinEnrollment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Teachers",
                nullable: false,
                defaultValue: new DateTime(2021, 8, 6, 12, 42, 42, 286, DateTimeKind.Local).AddTicks(4897),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 8, 3, 10, 34, 45, 464, DateTimeKind.Local).AddTicks(1077));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Examinations",
                nullable: false,
                defaultValue: new DateTime(2021, 8, 6, 12, 42, 42, 288, DateTimeKind.Local).AddTicks(6359),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 8, 3, 10, 34, 45, 466, DateTimeKind.Local).AddTicks(3411));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Examinations",
                nullable: false,
                defaultValue: new DateTime(2021, 8, 6, 12, 42, 42, 288, DateTimeKind.Local).AddTicks(8544),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 8, 3, 10, 34, 45, 466, DateTimeKind.Local).AddTicks(5351));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "ExamEnrollments",
                nullable: false,
                defaultValue: new DateTime(2021, 8, 6, 11, 42, 42, 290, DateTimeKind.Utc).AddTicks(4044),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 8, 3, 9, 34, 45, 468, DateTimeKind.Utc).AddTicks(1059));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ExamEnrollments",
                nullable: false,
                defaultValue: new DateTime(2021, 8, 6, 11, 42, 42, 290, DateTimeKind.Utc).AddTicks(2187),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 8, 3, 9, 34, 45, 467, DateTimeKind.Utc).AddTicks(9180));

            migrationBuilder.AddColumn<int>(
                name: "TotalStudentsEnrolled",
                table: "ExamEnrollments",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalStudentsEnrolled",
                table: "ExamEnrollments");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Teachers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 8, 3, 10, 34, 45, 464, DateTimeKind.Local).AddTicks(1077),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 8, 6, 12, 42, 42, 286, DateTimeKind.Local).AddTicks(4897));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Examinations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 8, 3, 10, 34, 45, 466, DateTimeKind.Local).AddTicks(3411),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 8, 6, 12, 42, 42, 288, DateTimeKind.Local).AddTicks(6359));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Examinations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 8, 3, 10, 34, 45, 466, DateTimeKind.Local).AddTicks(5351),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 8, 6, 12, 42, 42, 288, DateTimeKind.Local).AddTicks(8544));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "ExamEnrollments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 8, 3, 9, 34, 45, 468, DateTimeKind.Utc).AddTicks(1059),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 8, 6, 11, 42, 42, 290, DateTimeKind.Utc).AddTicks(4044));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ExamEnrollments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 8, 3, 9, 34, 45, 467, DateTimeKind.Utc).AddTicks(9180),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 8, 6, 11, 42, 42, 290, DateTimeKind.Utc).AddTicks(2187));
        }
    }
}
