using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EIMS.Migrations
{
    public partial class inspectorLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Teachers",
                nullable: false,
                defaultValue: new DateTime(2021, 8, 3, 10, 34, 45, 464, DateTimeKind.Local).AddTicks(1077),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 8, 2, 11, 1, 21, 46, DateTimeKind.Local).AddTicks(2322));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Examinations",
                nullable: false,
                defaultValue: new DateTime(2021, 8, 3, 10, 34, 45, 466, DateTimeKind.Local).AddTicks(3411),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 8, 2, 11, 1, 21, 60, DateTimeKind.Local).AddTicks(6754));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Examinations",
                nullable: false,
                defaultValue: new DateTime(2021, 8, 3, 10, 34, 45, 466, DateTimeKind.Local).AddTicks(5351),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 8, 2, 11, 1, 21, 61, DateTimeKind.Local).AddTicks(7863));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "ExamEnrollments",
                nullable: false,
                defaultValue: new DateTime(2021, 8, 3, 9, 34, 45, 468, DateTimeKind.Utc).AddTicks(1059),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 8, 2, 10, 1, 21, 69, DateTimeKind.Utc).AddTicks(6989));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ExamEnrollments",
                nullable: false,
                defaultValue: new DateTime(2021, 8, 3, 9, 34, 45, 467, DateTimeKind.Utc).AddTicks(9180),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 8, 2, 10, 1, 21, 69, DateTimeKind.Utc).AddTicks(401));

            migrationBuilder.CreateTable(
                name: "InspectorLogs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InspectorId = table.Column<int>(nullable: false),
                    Details = table.Column<string>(nullable: true),
                    LogType = table.Column<int>(nullable: false),
                    LocalGovernmentId = table.Column<int>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InspectorLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InspectorLogs_Inspectors_InspectorId",
                        column: x => x.InspectorId,
                        principalTable: "Inspectors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InspectorLogs_LocalGovernments_LocalGovernmentId",
                        column: x => x.LocalGovernmentId,
                        principalTable: "LocalGovernments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InspectorLogs_InspectorId",
                table: "InspectorLogs",
                column: "InspectorId");

            migrationBuilder.CreateIndex(
                name: "IX_InspectorLogs_LocalGovernmentId",
                table: "InspectorLogs",
                column: "LocalGovernmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InspectorLogs");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Teachers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 8, 2, 11, 1, 21, 46, DateTimeKind.Local).AddTicks(2322),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 8, 3, 10, 34, 45, 464, DateTimeKind.Local).AddTicks(1077));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Examinations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 8, 2, 11, 1, 21, 60, DateTimeKind.Local).AddTicks(6754),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 8, 3, 10, 34, 45, 466, DateTimeKind.Local).AddTicks(3411));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Examinations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 8, 2, 11, 1, 21, 61, DateTimeKind.Local).AddTicks(7863),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 8, 3, 10, 34, 45, 466, DateTimeKind.Local).AddTicks(5351));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "ExamEnrollments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 8, 2, 10, 1, 21, 69, DateTimeKind.Utc).AddTicks(6989),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 8, 3, 9, 34, 45, 468, DateTimeKind.Utc).AddTicks(1059));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ExamEnrollments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 8, 2, 10, 1, 21, 69, DateTimeKind.Utc).AddTicks(401),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 8, 3, 9, 34, 45, 467, DateTimeKind.Utc).AddTicks(9180));
        }
    }
}
