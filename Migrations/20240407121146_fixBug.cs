using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace kosarHospital.Migrations
{
    public partial class fixBug : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "roomNumber",
                table: "Time");

            migrationBuilder.AddColumn<string>(
                name: "detail",
                table: "Time",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "detail",
                table: "Time");

            migrationBuilder.AddColumn<int>(
                name: "roomNumber",
                table: "Time",
                type: "int",
                nullable: true);
        }
    }
}
