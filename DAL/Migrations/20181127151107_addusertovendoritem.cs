using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class addusertovendoritem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_VendorItems_UserId",
                table: "VendorItems",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_VendorItems_Users_UserId",
                table: "VendorItems",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VendorItems_Users_UserId",
                table: "VendorItems");

            migrationBuilder.DropIndex(
                name: "IX_VendorItems_UserId",
                table: "VendorItems");
        }
    }
}
