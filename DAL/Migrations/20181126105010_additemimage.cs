using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class additemimage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Thumb",
                table: "VendorItems",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Thumb",
                table: "VendorItems");
        }
    }
}
