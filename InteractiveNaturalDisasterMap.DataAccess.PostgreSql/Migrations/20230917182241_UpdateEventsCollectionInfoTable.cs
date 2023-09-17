using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InteractiveNaturalDisasterMap.DataAccess.PostgreSql.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEventsCollectionInfoTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EventsCollectionsInfo_CollectionName",
                table: "EventsCollectionsInfo");

            migrationBuilder.CreateIndex(
                name: "IX_EventsCollectionsInfo_CollectionName_UserId",
                table: "EventsCollectionsInfo",
                columns: new[] { "CollectionName", "UserId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EventsCollectionsInfo_CollectionName_UserId",
                table: "EventsCollectionsInfo");

            migrationBuilder.CreateIndex(
                name: "IX_EventsCollectionsInfo_CollectionName",
                table: "EventsCollectionsInfo",
                column: "CollectionName",
                unique: true);
        }
    }
}
