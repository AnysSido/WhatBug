using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class CreatePrioritySchemeRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PriorityPriorityScheme",
                columns: table => new
                {
                    PrioritiesId = table.Column<int>(type: "int", nullable: false),
                    PrioritySchemesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriorityPriorityScheme", x => new { x.PrioritiesId, x.PrioritySchemesId });
                    table.ForeignKey(
                        name: "FK_PriorityPriorityScheme_Priorities_PrioritiesId",
                        column: x => x.PrioritiesId,
                        principalTable: "Priorities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PriorityPriorityScheme_PrioritySchemes_PrioritySchemesId",
                        column: x => x.PrioritySchemesId,
                        principalTable: "PrioritySchemes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PriorityPriorityScheme_PrioritySchemesId",
                table: "PriorityPriorityScheme",
                column: "PrioritySchemesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PriorityPriorityScheme");
        }
    }
}
