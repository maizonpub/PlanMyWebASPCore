using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class galleryimage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "VendorItemGalleries",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "Image",
                table: "VendorItemGalleries",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
