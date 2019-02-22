using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class seatingchart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SeatsNumber",
                table: "GuestListTables",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TableName",
                table: "GuestListTables",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TableType",
                table: "GuestListTables",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SeatsNumber",
                table: "GuestListTables");

            migrationBuilder.DropColumn(
                name: "TableName",
                table: "GuestListTables");

            migrationBuilder.DropColumn(
                name: "TableType",
                table: "GuestListTables");
        }
    }
}
