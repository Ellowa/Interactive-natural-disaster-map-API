using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InteractiveNaturalDisasterMap.DataAccess.PostgreSql.Migrations
{
    /// <inheritdoc />
    public partial class MakeEventCollectionNameUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_EventsCollectionsInfo_CollectionName",
                table: "EventsCollectionsInfo",
                column: "CollectionName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EventsCollectionsInfo_CollectionName",
                table: "EventsCollectionsInfo");
        }
    }
}
