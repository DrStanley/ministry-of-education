using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EIMS.Migrations
{
    public partial class StudentLogTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Teachers",
                nullable: false,
                defaultValue: new DateTime(2021, 7, 7, 15, 57, 12, 924, DateTimeKind.Local).AddTicks(8861),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 7, 4, 17, 21, 36, 343, DateTimeKind.Local).AddTicks(2357));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Examinations",
                nullable: false,
                defaultValue: new DateTime(2021, 7, 7, 15, 57, 12, 930, DateTimeKind.Local).AddTicks(5112),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 7, 4, 17, 21, 36, 348, DateTimeKind.Local).AddTicks(9068));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Examinations",
                nullable: false,
                defaultValue: new DateTime(2021, 7, 7, 15, 57, 12, 930, DateTimeKind.Local).AddTicks(9966),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 7, 4, 17, 21, 36, 349, DateTimeKind.Local).AddTicks(3987));

            migrationBuilder.CreateTable(
                name: "StudentLogs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(nullable: false),
                    SchoolId = table.Column<int>(nullable: true),
                    ClassType = table.Column<int>(nullable: false),
                    ClassesId = table.Column<int>(nullable: true),
                    Details = table.Column<string>(nullable: true),
                    LogType = table.Column<int>(nullable: false),
                    LocalGovernmentId = table.Column<int>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentLogs_Classes_ClassesId",
                        column: x => x.ClassesId,
                        principalTable: "Classes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentLogs_LocalGovernments_LocalGovernmentId",
                        column: x => x.LocalGovernmentId,
                        principalTable: "LocalGovernments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentLogs_Schools_SchoolId",
                        column: x => x.SchoolId,
                        principalTable: "Schools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentLogs_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentLogs_ClassesId",
                table: "StudentLogs",
                column: "ClassesId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentLogs_LocalGovernmentId",
                table: "StudentLogs",
                column: "LocalGovernmentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentLogs_SchoolId",
                table: "StudentLogs",
                column: "SchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentLogs_StudentId",
                table: "StudentLogs",
                column: "StudentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentLogs");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Teachers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 7, 4, 17, 21, 36, 343, DateTimeKind.Local).AddTicks(2357),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 7, 7, 15, 57, 12, 924, DateTimeKind.Local).AddTicks(8861));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Examinations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 7, 4, 17, 21, 36, 348, DateTimeKind.Local).AddTicks(9068),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 7, 7, 15, 57, 12, 930, DateTimeKind.Local).AddTicks(5112));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Examinations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 7, 4, 17, 21, 36, 349, DateTimeKind.Local).AddTicks(3987),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 7, 7, 15, 57, 12, 930, DateTimeKind.Local).AddTicks(9966));
        }
    }
}
