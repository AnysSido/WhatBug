using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class AddPriorityAndSchemes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PrioritySchemeId",
                table: "Projects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Priorities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Priorities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PrioritySchemes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrioritySchemes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Projects_PrioritySchemeId",
                table: "Projects",
                column: "PrioritySchemeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_PrioritySchemes_PrioritySchemeId",
                table: "Projects",
                column: "PrioritySchemeId",
                principalTable: "PrioritySchemes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_PrioritySchemes_PrioritySchemeId",
                table: "Projects");

            migrationBuilder.DropTable(
                name: "Priorities");

            migrationBuilder.DropTable(
                name: "PrioritySchemes");

            migrationBuilder.DropIndex(
                name: "IX_Projects_PrioritySchemeId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "PrioritySchemeId",
                table: "Projects");
        }
    }
}
