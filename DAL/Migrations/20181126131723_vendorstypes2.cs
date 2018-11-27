using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class vendorstypes2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VendorItemTypeValues_VendorItems_VendorItemId",
                table: "VendorItemTypeValues");

            migrationBuilder.AlterColumn<int>(
                name: "VendorItemId",
                table: "VendorItemTypeValues",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_VendorItemTypeValues_VendorItems_VendorItemId",
                table: "VendorItemTypeValues",
                column: "VendorItemId",
                principalTable: "VendorItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VendorItemTypeValues_VendorItems_VendorItemId",
                table: "VendorItemTypeValues");

            migrationBuilder.AlterColumn<int>(
                name: "VendorItemId",
                table: "VendorItemTypeValues",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_VendorItemTypeValues_VendorItems_VendorItemId",
                table: "VendorItemTypeValues",
                column: "VendorItemId",
                principalTable: "VendorItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
