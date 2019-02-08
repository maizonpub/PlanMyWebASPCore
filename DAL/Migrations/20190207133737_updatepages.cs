using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class updatepages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HtmlText",
                table: "Pages",
                newName: "HtmlDescription");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HtmlDescription",
                table: "Pages",
                newName: "HtmlText");
        }
    }
}
