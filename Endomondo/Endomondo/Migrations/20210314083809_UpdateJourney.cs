using Microsoft.EntityFrameworkCore.Migrations;

namespace Endomondo.Migrations
{
    public partial class UpdateJourney : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Journeys_JourneyId",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "JoruneyId",
                table: "Locations");

            migrationBuilder.AlterColumn<int>(
                name: "JourneyId",
                table: "Locations",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_Journeys_JourneyId",
                table: "Locations",
                column: "JourneyId",
                principalTable: "Journeys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Journeys_JourneyId",
                table: "Locations");

            migrationBuilder.AlterColumn<int>(
                name: "JourneyId",
                table: "Locations",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<int>(
                name: "JoruneyId",
                table: "Locations",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_Journeys_JourneyId",
                table: "Locations",
                column: "JourneyId",
                principalTable: "Journeys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
