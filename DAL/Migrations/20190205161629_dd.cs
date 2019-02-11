using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class dd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VendorItemReviews_VendorItems_VendorItemId",
                table: "VendorItemReviews");

            migrationBuilder.AlterColumn<int>(
                name: "VendorItemId",
                table: "VendorItemReviews",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

           

            migrationBuilder.AddForeignKey(
                name: "FK_VendorItemReviews_VendorItems_VendorItemId",
                table: "VendorItemReviews",
                column: "VendorItemId",
                principalTable: "VendorItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VendorItemReviews_VendorItems_VendorItemId",
                table: "VendorItemReviews");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "VendorItemGalleries");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "OffersGalleries");

            migrationBuilder.AlterColumn<int>(
                name: "VendorItemId",
                table: "VendorItemReviews",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_VendorItemReviews_VendorItems_VendorItemId",
                table: "VendorItemReviews",
                column: "VendorItemId",
                principalTable: "VendorItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
