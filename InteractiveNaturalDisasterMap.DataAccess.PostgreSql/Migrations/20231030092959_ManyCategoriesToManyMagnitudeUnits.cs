using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InteractiveNaturalDisasterMap.DataAccess.PostgreSql.Migrations
{
    /// <inheritdoc />
    public partial class ManyCategoriesToManyMagnitudeUnits : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EventCategoryMagnitudeUnit",
                columns: table => new
                {
                    EventCategoriesId = table.Column<int>(type: "integer", nullable: false),
                    MagnitudeUnitsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventCategoryMagnitudeUnit", x => new { x.EventCategoriesId, x.MagnitudeUnitsId });
                    table.ForeignKey(
                        name: "FK_EventCategoryMagnitudeUnit_EventsCategories_EventCategories~",
                        column: x => x.EventCategoriesId,
                        principalTable: "EventsCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventCategoryMagnitudeUnit_MagnitudeUnits_MagnitudeUnitsId",
                        column: x => x.MagnitudeUnitsId,
                        principalTable: "MagnitudeUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventCategoryMagnitudeUnit_MagnitudeUnitsId",
                table: "EventCategoryMagnitudeUnit",
                column: "MagnitudeUnitsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventCategoryMagnitudeUnit");
        }
    }
}
