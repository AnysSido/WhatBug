using Microsoft.EntityFrameworkCore.Migrations;

namespace WhatBug.Persistence.Migrations
{
    public partial class RefactorPrioritySchemePriority : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PriorityPriorityScheme");

            migrationBuilder.CreateTable(
                name: "PrioritySchemePriorities",
                columns: table => new
                {
                    PrioritySchemeId = table.Column<int>(type: "integer", nullable: false),
                    PriorityId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrioritySchemePriorities", x => new { x.PrioritySchemeId, x.PriorityId });
                    table.ForeignKey(
                        name: "FK_PrioritySchemePriorities_Priorities_PriorityId",
                        column: x => x.PriorityId,
                        principalTable: "Priorities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PrioritySchemePriorities_PrioritySchemes_PrioritySchemeId",
                        column: x => x.PrioritySchemeId,
                        principalTable: "PrioritySchemes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PrioritySchemePriorities_PriorityId",
                table: "PrioritySchemePriorities",
                column: "PriorityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PrioritySchemePriorities");

            migrationBuilder.CreateTable(
                name: "PriorityPriorityScheme",
                columns: table => new
                {
                    PrioritiesId = table.Column<int>(type: "integer", nullable: false),
                    PrioritySchemesId = table.Column<int>(type: "integer", nullable: false)
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
    }
}
