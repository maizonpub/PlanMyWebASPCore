using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class gallery : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BlogId",
                table: "BlogGalleries",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "BlogGalleries",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BlogGalleries_BlogId",
                table: "BlogGalleries",
                column: "BlogId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogGalleries_Blogs_BlogId",
                table: "BlogGalleries",
                column: "BlogId",
                principalTable: "Blogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogGalleries_Blogs_BlogId",
                table: "BlogGalleries");

            migrationBuilder.DropIndex(
                name: "IX_BlogGalleries_BlogId",
                table: "BlogGalleries");

            migrationBuilder.DropColumn(
                name: "BlogId",
                table: "BlogGalleries");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "BlogGalleries");
        }
    }
}
