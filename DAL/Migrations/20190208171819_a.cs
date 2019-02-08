using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class a : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VendorBranches_VendorItems_VendorItemId",
                table: "VendorBranches");

            migrationBuilder.AlterColumn<int>(
                name: "VendorItemId",
                table: "VendorBranches",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "VendorBranches",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_VendorBranches_VendorItems_VendorItemId",
                table: "VendorBranches",
                column: "VendorItemId",
                principalTable: "VendorItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VendorBranches_VendorItems_VendorItemId",
                table: "VendorBranches");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "VendorBranches");

            migrationBuilder.AlterColumn<int>(
                name: "VendorItemId",
                table: "VendorBranches",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_VendorBranches_VendorItems_VendorItemId",
                table: "VendorBranches",
                column: "VendorItemId",
                principalTable: "VendorItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
