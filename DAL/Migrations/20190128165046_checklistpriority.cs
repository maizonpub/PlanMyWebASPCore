using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class checklistpriority : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GuestLists_GuestListTables_GuestListTablesId",
                table: "GuestLists");

            migrationBuilder.DropForeignKey(
                name: "FK_VendorTypes_VendorCategories_VendorCategoryId",
                table: "VendorTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_WishLists_VendorItems_VendorItemId",
                table: "WishLists");

            migrationBuilder.AlterColumn<int>(
                name: "VendorItemId",
                table: "WishLists",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "VendorCategoryId",
                table: "VendorTypes",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "GuestListTablesId",
                table: "GuestLists",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPriority",
                table: "CheckLists",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_GuestLists_GuestListTables_GuestListTablesId",
                table: "GuestLists",
                column: "GuestListTablesId",
                principalTable: "GuestListTables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VendorTypes_VendorCategories_VendorCategoryId",
                table: "VendorTypes",
                column: "VendorCategoryId",
                principalTable: "VendorCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WishLists_VendorItems_VendorItemId",
                table: "WishLists",
                column: "VendorItemId",
                principalTable: "VendorItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GuestLists_GuestListTables_GuestListTablesId",
                table: "GuestLists");

            migrationBuilder.DropForeignKey(
                name: "FK_VendorTypes_VendorCategories_VendorCategoryId",
                table: "VendorTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_WishLists_VendorItems_VendorItemId",
                table: "WishLists");

            migrationBuilder.DropColumn(
                name: "IsPriority",
                table: "CheckLists");

            migrationBuilder.AlterColumn<int>(
                name: "VendorItemId",
                table: "WishLists",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "VendorCategoryId",
                table: "VendorTypes",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "GuestListTablesId",
                table: "GuestLists",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_GuestLists_GuestListTables_GuestListTablesId",
                table: "GuestLists",
                column: "GuestListTablesId",
                principalTable: "GuestListTables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VendorTypes_VendorCategories_VendorCategoryId",
                table: "VendorTypes",
                column: "VendorCategoryId",
                principalTable: "VendorCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WishLists_VendorItems_VendorItemId",
                table: "WishLists",
                column: "VendorItemId",
                principalTable: "VendorItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
