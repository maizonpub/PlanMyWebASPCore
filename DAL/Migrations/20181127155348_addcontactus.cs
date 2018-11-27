using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class addcontactus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "VendorCategories",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "SalePrice",
                table: "Offers",
                nullable: true,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "Offers",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Offers_UserId",
                table: "Offers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_Users_UserId",
                table: "Offers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offers_Users_UserId",
                table: "Offers");

            migrationBuilder.DropIndex(
                name: "IX_Offers_UserId",
                table: "Offers");

            migrationBuilder.AlterColumn<byte[]>(
                name: "Image",
                table: "VendorCategories",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "SalePrice",
                table: "Offers",
                nullable: false,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "Image",
                table: "Offers",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
