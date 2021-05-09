using Microsoft.EntityFrameworkCore.Migrations;

namespace Phonebook.Report.Migrations
{
    public partial class LocationRelationUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locations_ReportDetails_ReportDetailId",
                table: "Locations");

            migrationBuilder.RenameColumn(
                name: "ReportDetailId",
                table: "Locations",
                newName: "ReportId");

            migrationBuilder.RenameIndex(
                name: "IX_Locations_ReportDetailId",
                table: "Locations",
                newName: "IX_Locations_ReportId");

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_Reports_ReportId",
                table: "Locations",
                column: "ReportId",
                principalTable: "Reports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Reports_ReportId",
                table: "Locations");

            migrationBuilder.RenameColumn(
                name: "ReportId",
                table: "Locations",
                newName: "ReportDetailId");

            migrationBuilder.RenameIndex(
                name: "IX_Locations_ReportId",
                table: "Locations",
                newName: "IX_Locations_ReportDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_ReportDetails_ReportDetailId",
                table: "Locations",
                column: "ReportDetailId",
                principalTable: "ReportDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
