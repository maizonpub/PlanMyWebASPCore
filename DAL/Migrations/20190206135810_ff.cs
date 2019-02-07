using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class ff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Facebook",
                table: "VendorItems",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Instagram",
                table: "VendorItems",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Website",
                table: "VendorItems",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Facebook",
                table: "VendorItems");

            migrationBuilder.DropColumn(
                name: "Instagram",
                table: "VendorItems");

            migrationBuilder.DropColumn(
                name: "Website",
                table: "VendorItems");
        }
    }
}
