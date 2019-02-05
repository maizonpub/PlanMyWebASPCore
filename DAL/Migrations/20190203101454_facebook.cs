using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class facebook : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FBToken",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FBToken",
                table: "AspNetUsers");
        }
    }
}
