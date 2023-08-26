using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace InteractiveNaturalDisasterMap.DataAccess.PostgreSql.Migrations
{
    /// <inheritdoc />
    public partial class AddEventHazardUnitsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EventHazardUnitId",
                table: "NaturalDisasterEvents",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "EventHazardUnits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    HazardName = table.Column<string>(type: "text", nullable: false),
                    MagnitudeUnitId = table.Column<int>(type: "integer", nullable: false),
                    ThresholdValue = table.Column<double>(type: "double precision", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventHazardUnits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventHazardUnits_MagnitudeUnits_MagnitudeUnitId",
                        column: x => x.MagnitudeUnitId,
                        principalTable: "MagnitudeUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NaturalDisasterEvents_EventHazardUnitId",
                table: "NaturalDisasterEvents",
                column: "EventHazardUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_EventHazardUnits_MagnitudeUnitId",
                table: "EventHazardUnits",
                column: "MagnitudeUnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_NaturalDisasterEvents_EventHazardUnits_EventHazardUnitId",
                table: "NaturalDisasterEvents",
                column: "EventHazardUnitId",
                principalTable: "EventHazardUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NaturalDisasterEvents_EventHazardUnits_EventHazardUnitId",
                table: "NaturalDisasterEvents");

            migrationBuilder.DropTable(
                name: "EventHazardUnits");

            migrationBuilder.DropIndex(
                name: "IX_NaturalDisasterEvents_EventHazardUnitId",
                table: "NaturalDisasterEvents");

            migrationBuilder.DropColumn(
                name: "EventHazardUnitId",
                table: "NaturalDisasterEvents");
        }
    }
}
