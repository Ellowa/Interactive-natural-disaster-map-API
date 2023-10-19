using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InteractiveNaturalDisasterMap.DataAccess.PostgreSql.Migrations
{
    /// <inheritdoc />
    public partial class AddIdInThirdPartyApiFieldUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_NaturalDisasterEvents_IdInThirdPartyApi",
                table: "NaturalDisasterEvents",
                column: "IdInThirdPartyApi",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_NaturalDisasterEvents_IdInThirdPartyApi",
                table: "NaturalDisasterEvents");
        }
    }
}
