using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class vendorstypes1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VendorTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    CategoryId = table.Column<int>(nullable: false),
                    VendorCategory = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendorTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VendorTypeValues",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    VendorTypeId = table.Column<int>(nullable: true),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendorTypeValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VendorTypeValues_VendorTypes_VendorTypeId",
                        column: x => x.VendorTypeId,
                        principalTable: "VendorTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VendorItemTypeValues",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    VendorTypeValueId = table.Column<int>(nullable: true),
                    VendorItemId = table.Column<int>(nullable: true),
                    VendorTypeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendorItemTypeValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VendorItemTypeValues_VendorItems_VendorItemId",
                        column: x => x.VendorItemId,
                        principalTable: "VendorItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VendorItemTypeValues_VendorTypes_VendorTypeId",
                        column: x => x.VendorTypeId,
                        principalTable: "VendorTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VendorItemTypeValues_VendorTypeValues_VendorTypeValueId",
                        column: x => x.VendorTypeValueId,
                        principalTable: "VendorTypeValues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VendorItemTypeValues_VendorItemId",
                table: "VendorItemTypeValues",
                column: "VendorItemId");

            migrationBuilder.CreateIndex(
                name: "IX_VendorItemTypeValues_VendorTypeId",
                table: "VendorItemTypeValues",
                column: "VendorTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_VendorItemTypeValues_VendorTypeValueId",
                table: "VendorItemTypeValues",
                column: "VendorTypeValueId");

            migrationBuilder.CreateIndex(
                name: "IX_VendorTypeValues_VendorTypeId",
                table: "VendorTypeValues",
                column: "VendorTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VendorItemTypeValues");

            migrationBuilder.DropTable(
                name: "VendorTypeValues");

            migrationBuilder.DropTable(
                name: "VendorTypes");
        }
    }
}
