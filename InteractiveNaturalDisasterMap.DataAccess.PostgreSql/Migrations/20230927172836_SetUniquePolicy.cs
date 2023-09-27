using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InteractiveNaturalDisasterMap.DataAccess.PostgreSql.Migrations
{
    /// <inheritdoc />
    public partial class SetUniquePolicy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Users_Login",
                table: "Users",
                column: "Login",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleName",
                table: "UserRoles",
                column: "RoleName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MagnitudeUnits_MagnitudeUnitName",
                table: "MagnitudeUnits",
                column: "MagnitudeUnitName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EventSources_SourceType",
                table: "EventSources",
                column: "SourceType",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EventsCategories_CategoryName",
                table: "EventsCategories",
                column: "CategoryName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EventHazardUnits_HazardName_MagnitudeUnitId",
                table: "EventHazardUnits",
                columns: new[] { "HazardName", "MagnitudeUnitId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EventHazardUnits_ThresholdValue_MagnitudeUnitId",
                table: "EventHazardUnits",
                columns: new[] { "ThresholdValue", "MagnitudeUnitId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_Login",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_UserRoles_RoleName",
                table: "UserRoles");

            migrationBuilder.DropIndex(
                name: "IX_MagnitudeUnits_MagnitudeUnitName",
                table: "MagnitudeUnits");

            migrationBuilder.DropIndex(
                name: "IX_EventSources_SourceType",
                table: "EventSources");

            migrationBuilder.DropIndex(
                name: "IX_EventsCategories_CategoryName",
                table: "EventsCategories");

            migrationBuilder.DropIndex(
                name: "IX_EventHazardUnits_HazardName_MagnitudeUnitId",
                table: "EventHazardUnits");

            migrationBuilder.DropIndex(
                name: "IX_EventHazardUnits_ThresholdValue_MagnitudeUnitId",
                table: "EventHazardUnits");
        }
    }
}
