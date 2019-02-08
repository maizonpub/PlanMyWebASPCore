using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class im : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Thumb",
                table: "Careers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WideImage",
                table: "Careers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Thumb",
                table: "Careers");

            migrationBuilder.DropColumn(
                name: "WideImage",
                table: "Careers");
        }
    }
}
