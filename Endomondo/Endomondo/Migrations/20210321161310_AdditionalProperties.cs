using Microsoft.EntityFrameworkCore.Migrations;

namespace Endomondo.Migrations
{
    public partial class AdditionalProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Speed",
                table: "Locations",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "AverageSpeed",
                table: "Journeys",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "BurnedCalories",
                table: "Journeys",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "MaxSpeed",
                table: "Journeys",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfSteps",
                table: "Journeys",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Speed",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "AverageSpeed",
                table: "Journeys");

            migrationBuilder.DropColumn(
                name: "BurnedCalories",
                table: "Journeys");

            migrationBuilder.DropColumn(
                name: "MaxSpeed",
                table: "Journeys");

            migrationBuilder.DropColumn(
                name: "NumberOfSteps",
                table: "Journeys");
        }
    }
}
