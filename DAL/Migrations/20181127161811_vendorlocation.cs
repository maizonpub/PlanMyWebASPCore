using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class vendorlocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Longitude",
                table: "VendorItems",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<double>(
                name: "Latitude",
                table: "VendorItems",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "VendorItems",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "VendorItems");

            migrationBuilder.AlterColumn<double>(
                name: "Longitude",
                table: "VendorItems",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Latitude",
                table: "VendorItems",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);
        }
    }
}
