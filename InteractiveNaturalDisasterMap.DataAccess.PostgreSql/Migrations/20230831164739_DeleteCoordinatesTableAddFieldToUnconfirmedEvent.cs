using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace InteractiveNaturalDisasterMap.DataAccess.PostgreSql.Migrations
{
    /// <inheritdoc />
    public partial class DeleteCoordinatesTableAddFieldToUnconfirmedEvent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NaturalDisasterEvents_EventCoordinates_CoordinateId",
                table: "NaturalDisasterEvents");

            migrationBuilder.DropTable(
                name: "EventCoordinates");

            migrationBuilder.DropIndex(
                name: "IX_NaturalDisasterEvents_CoordinateId",
                table: "NaturalDisasterEvents");

            migrationBuilder.DropColumn(
                name: "CoordinateId",
                table: "NaturalDisasterEvents");

            migrationBuilder.AddColumn<bool>(
                name: "IsChecked",
                table: "UnconfirmedEvents",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "NaturalDisasterEvents",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "NaturalDisasterEvents",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsChecked",
                table: "UnconfirmedEvents");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "NaturalDisasterEvents");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "NaturalDisasterEvents");

            migrationBuilder.AddColumn<int>(
                name: "CoordinateId",
                table: "NaturalDisasterEvents",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "EventCoordinates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Latitude = table.Column<double>(type: "double precision", nullable: false),
                    Longitude = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventCoordinates", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NaturalDisasterEvents_CoordinateId",
                table: "NaturalDisasterEvents",
                column: "CoordinateId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_NaturalDisasterEvents_EventCoordinates_CoordinateId",
                table: "NaturalDisasterEvents",
                column: "CoordinateId",
                principalTable: "EventCoordinates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
