using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InteractiveNaturalDisasterMap.DataAccess.PostgreSql.Migrations
{
    /// <inheritdoc />
    public partial class AddUnconfirmedEventsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Telegram",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UnconfirmedEvents",
                columns: table => new
                {
                    EventId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnconfirmedEvents", x => x.EventId);
                    table.ForeignKey(
                        name: "FK_UnconfirmedEvents_NaturalDisasterEvents_EventId",
                        column: x => x.EventId,
                        principalTable: "NaturalDisasterEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UnconfirmedEvents_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UnconfirmedEvents_UserId",
                table: "UnconfirmedEvents",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UnconfirmedEvents");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Telegram",
                table: "Users");
        }
    }
}
