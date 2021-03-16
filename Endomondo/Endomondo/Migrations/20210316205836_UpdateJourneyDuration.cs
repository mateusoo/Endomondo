using Microsoft.EntityFrameworkCore.Migrations;

namespace Endomondo.Migrations
{
    public partial class UpdateJourneyDuration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateTime",
                table: "Journeys",
                newName: "StartDateTime");

            migrationBuilder.AlterColumn<long>(
                name: "Duration",
                table: "Journeys",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "REAL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartDateTime",
                table: "Journeys",
                newName: "DateTime");

            migrationBuilder.AlterColumn<double>(
                name: "Duration",
                table: "Journeys",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "INTEGER");
        }
    }
}
