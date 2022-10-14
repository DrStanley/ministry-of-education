using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EIMS.Migrations
{
    public partial class StateEnum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Teachers",
                nullable: false,
                defaultValue: new DateTime(2021, 8, 12, 12, 42, 43, 976, DateTimeKind.Local).AddTicks(6707),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 8, 6, 12, 42, 42, 286, DateTimeKind.Local).AddTicks(4897));

            migrationBuilder.AlterColumn<int>(
                name: "StateOfOrigin",
                table: "Students",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "StateOfOrigin",
                table: "Profiles",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Examinations",
                nullable: false,
                defaultValue: new DateTime(2021, 8, 12, 12, 42, 43, 978, DateTimeKind.Local).AddTicks(8819),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 8, 6, 12, 42, 42, 288, DateTimeKind.Local).AddTicks(6359));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Examinations",
                nullable: false,
                defaultValue: new DateTime(2021, 8, 12, 12, 42, 43, 979, DateTimeKind.Local).AddTicks(741),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 8, 6, 12, 42, 42, 288, DateTimeKind.Local).AddTicks(8544));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "ExamEnrollments",
                nullable: false,
                defaultValue: new DateTime(2021, 8, 12, 11, 42, 43, 980, DateTimeKind.Utc).AddTicks(6099),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 8, 6, 11, 42, 42, 290, DateTimeKind.Utc).AddTicks(4044));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ExamEnrollments",
                nullable: false,
                defaultValue: new DateTime(2021, 8, 12, 11, 42, 43, 980, DateTimeKind.Utc).AddTicks(4211),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 8, 6, 11, 42, 42, 290, DateTimeKind.Utc).AddTicks(2187));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Teachers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 8, 6, 12, 42, 42, 286, DateTimeKind.Local).AddTicks(4897),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 8, 12, 12, 42, 43, 976, DateTimeKind.Local).AddTicks(6707));

            migrationBuilder.AlterColumn<string>(
                name: "StateOfOrigin",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "StateOfOrigin",
                table: "Profiles",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Examinations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 8, 6, 12, 42, 42, 288, DateTimeKind.Local).AddTicks(6359),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 8, 12, 12, 42, 43, 978, DateTimeKind.Local).AddTicks(8819));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Examinations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 8, 6, 12, 42, 42, 288, DateTimeKind.Local).AddTicks(8544),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 8, 12, 12, 42, 43, 979, DateTimeKind.Local).AddTicks(741));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "ExamEnrollments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 8, 6, 11, 42, 42, 290, DateTimeKind.Utc).AddTicks(4044),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 8, 12, 11, 42, 43, 980, DateTimeKind.Utc).AddTicks(6099));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ExamEnrollments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 8, 6, 11, 42, 42, 290, DateTimeKind.Utc).AddTicks(2187),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 8, 12, 11, 42, 43, 980, DateTimeKind.Utc).AddTicks(4211));
        }
    }
}
