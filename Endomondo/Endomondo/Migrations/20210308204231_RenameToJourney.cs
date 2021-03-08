using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Endomondo.Migrations
{
    public partial class RenameToJourney : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Routes_RouteId",
                table: "Locations");

            migrationBuilder.DropTable(
                name: "Routes");

            migrationBuilder.DropIndex(
                name: "IX_Locations_RouteId",
                table: "Locations");

            migrationBuilder.RenameColumn(
                name: "RouteId",
                table: "Locations",
                newName: "JoruneyId");

            migrationBuilder.AddColumn<int>(
                name: "JourneyId",
                table: "Locations",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Journeys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Duration = table.Column<double>(type: "REAL", nullable: false),
                    DateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Distance = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Journeys", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Locations_JourneyId",
                table: "Locations",
                column: "JourneyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_Journeys_JourneyId",
                table: "Locations",
                column: "JourneyId",
                principalTable: "Journeys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Journeys_JourneyId",
                table: "Locations");

            migrationBuilder.DropTable(
                name: "Journeys");

            migrationBuilder.DropIndex(
                name: "IX_Locations_JourneyId",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "JourneyId",
                table: "Locations");

            migrationBuilder.RenameColumn(
                name: "JoruneyId",
                table: "Locations",
                newName: "RouteId");

            migrationBuilder.CreateTable(
                name: "Routes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Duration = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Routes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Locations_RouteId",
                table: "Locations",
                column: "RouteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_Routes_RouteId",
                table: "Locations",
                column: "RouteId",
                principalTable: "Routes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
