using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class dbfixlocal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GuestLists_GuestListTables_GuestListTablesId",
                table: "GuestLists");

            migrationBuilder.DropForeignKey(
                name: "FK_VendorTypes_VendorCategories_VendorCategoryId",
                table: "VendorTypes");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GuestLists_GuestListTables_GuestListTablesId",
                table: "GuestLists");

            migrationBuilder.DropForeignKey(
                name: "FK_VendorTypes_VendorCategories_VendorCategoryId",
                table: "VendorTypes");

            migrationBuilder.AlterColumn<int>(
                name: "VendorCategoryId",
                table: "VendorTypes",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "GuestListTablesId",
                table: "GuestLists",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

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
        }
    }
}
