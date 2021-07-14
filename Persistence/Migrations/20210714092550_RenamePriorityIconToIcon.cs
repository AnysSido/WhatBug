using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class RenamePriorityIconToIcon : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Priorities_PriorityIcons_PriorityIconId",
                table: "Priorities");

            migrationBuilder.DropTable(
                name: "PriorityIcons");

            migrationBuilder.RenameColumn(
                name: "PriorityIconId",
                table: "Priorities",
                newName: "IconId");

            migrationBuilder.RenameIndex(
                name: "IX_Priorities_PriorityIconId",
                table: "Priorities",
                newName: "IX_Priorities_IconId");

            migrationBuilder.CreateTable(
                name: "Icons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Icons", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Icons",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "ArrowUp" },
                    { 24, "XMark" },
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
                    { 13, "AngleUp" },
                    { 12, "ChevronRight" },
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
                    { 25, "Ban" },
                    { 26, "Equals" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Priorities_Icons_IconId",
                table: "Priorities",
                column: "IconId",
                principalTable: "Icons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Priorities_Icons_IconId",
                table: "Priorities");

            migrationBuilder.DropTable(
                name: "Icons");

            migrationBuilder.RenameColumn(
                name: "IconId",
                table: "Priorities",
                newName: "PriorityIconId");

            migrationBuilder.RenameIndex(
                name: "IX_Priorities_IconId",
                table: "Priorities",
                newName: "IX_Priorities_PriorityIconId");

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
                    { 24, "XMark" },
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
                    { 13, "AngleUp" },
                    { 12, "ChevronRight" },
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
                    { 25, "Ban" },
                    { 26, "Equals" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Priorities_PriorityIcons_PriorityIconId",
                table: "Priorities",
                column: "PriorityIconId",
                principalTable: "PriorityIcons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
