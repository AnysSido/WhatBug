using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class CreatePriorityIconsTableWithSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PriorityIconId",
                table: "Priorities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PriorityIcons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriorityIcons", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "PriorityIcons",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "ArrowUp" },
                    { 23, "TriangleExclaimation" },
                    { 22, "CircleExclaimation" },
                    { 21, "Exclaimation" },
                    { 20, "AnglesRight" },
                    { 19, "AnglesLeft" },
                    { 18, "AnglesDown" },
                    { 17, "AnglesUp" },
                    { 16, "AngleRight" },
                    { 15, "AngleLeft" },
                    { 14, "AngleDown" },
                    { 24, "XMark" },
                    { 13, "AngleUp" },
                    { 11, "ChevronLeft" },
                    { 10, "ChevronDown" },
                    { 9, "ChevronUp" },
                    { 8, "CircleArrowRight" },
                    { 7, "CircleArrowLeft" },
                    { 6, "CircleArrowDown" },
                    { 5, "CircleArrowUp" },
                    { 4, "ArrowRight" },
                    { 3, "ArrowLeft" },
                    { 2, "ArrowDown" },
                    { 12, "ChevronRight" },
                    { 25, "Ban" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Priorities_PriorityIconId",
                table: "Priorities",
                column: "PriorityIconId");

            migrationBuilder.AddForeignKey(
                name: "FK_Priorities_PriorityIcons_PriorityIconId",
                table: "Priorities",
                column: "PriorityIconId",
                principalTable: "PriorityIcons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Priorities_PriorityIcons_PriorityIconId",
                table: "Priorities");

            migrationBuilder.DropTable(
                name: "PriorityIcons");

            migrationBuilder.DropIndex(
                name: "IX_Priorities_PriorityIconId",
                table: "Priorities");

            migrationBuilder.DropColumn(
                name: "PriorityIconId",
                table: "Priorities");
        }
    }
}
