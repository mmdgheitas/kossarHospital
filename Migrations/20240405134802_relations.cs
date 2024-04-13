using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace kosarHospital.Migrations
{
    public partial class relations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "userId",
                table: "Time",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Time_userId",
                table: "Time",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_Time_AspNetUsers_userId",
                table: "Time",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Time_AspNetUsers_userId",
                table: "Time");

            migrationBuilder.DropIndex(
                name: "IX_Time_userId",
                table: "Time");

            migrationBuilder.DropColumn(
                name: "userId",
                table: "Time");
        }
    }
}
