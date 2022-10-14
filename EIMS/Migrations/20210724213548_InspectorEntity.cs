using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EIMS.Migrations
{
    public partial class InspectorEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Teachers",
                nullable: false,
                defaultValue: new DateTime(2021, 7, 24, 22, 35, 48, 310, DateTimeKind.Local).AddTicks(463),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 7, 20, 17, 19, 32, 122, DateTimeKind.Local).AddTicks(2349));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Examinations",
                nullable: false,
                defaultValue: new DateTime(2021, 7, 24, 22, 35, 48, 312, DateTimeKind.Local).AddTicks(3970),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 7, 20, 17, 19, 32, 124, DateTimeKind.Local).AddTicks(4481));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Examinations",
                nullable: false,
                defaultValue: new DateTime(2021, 7, 24, 22, 35, 48, 312, DateTimeKind.Local).AddTicks(5840),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 7, 20, 17, 19, 32, 124, DateTimeKind.Local).AddTicks(6493));

            migrationBuilder.CreateTable(
                name: "Inspectors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: true),
                    LocalGovernmentId = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inspectors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inspectors_LocalGovernments_LocalGovernmentId",
                        column: x => x.LocalGovernmentId,
                        principalTable: "LocalGovernments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Inspectors_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Inspectors_LocalGovernmentId",
                table: "Inspectors",
                column: "LocalGovernmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Inspectors_UserId",
                table: "Inspectors",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Inspectors");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Teachers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 7, 20, 17, 19, 32, 122, DateTimeKind.Local).AddTicks(2349),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 7, 24, 22, 35, 48, 310, DateTimeKind.Local).AddTicks(463));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Examinations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 7, 20, 17, 19, 32, 124, DateTimeKind.Local).AddTicks(4481),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 7, 24, 22, 35, 48, 312, DateTimeKind.Local).AddTicks(3970));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Examinations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 7, 20, 17, 19, 32, 124, DateTimeKind.Local).AddTicks(6493),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 7, 24, 22, 35, 48, 312, DateTimeKind.Local).AddTicks(5840));
        }
    }
}
