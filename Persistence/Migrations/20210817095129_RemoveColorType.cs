using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class RemoveColorType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IssueTypes_ColorIcons_ColorIconId",
                table: "IssueTypes");

            migrationBuilder.DropTable(
                name: "ColorIcons");

            migrationBuilder.RenameColumn(
                name: "ColorIconId",
                table: "IssueTypes",
                newName: "IconId");

            migrationBuilder.RenameIndex(
                name: "IX_IssueTypes_ColorIconId",
                table: "IssueTypes",
                newName: "IX_IssueTypes_IconId");

            migrationBuilder.AddColumn<int>(
                name: "ColorId",
                table: "IssueTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "IssueTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ColorId", "IconId" },
                values: new object[] { 34, 29 });

            migrationBuilder.UpdateData(
                table: "IssueTypes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ColorId", "IconId" },
                values: new object[] { 19, 27 });

            migrationBuilder.CreateIndex(
                name: "IX_IssueTypes_ColorId",
                table: "IssueTypes",
                column: "ColorId");

            migrationBuilder.AddForeignKey(
                name: "FK_IssueTypes_Colors_ColorId",
                table: "IssueTypes",
                column: "ColorId",
                principalTable: "Colors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IssueTypes_Icons_IconId",
                table: "IssueTypes",
                column: "IconId",
                principalTable: "Icons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IssueTypes_Colors_ColorId",
                table: "IssueTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_IssueTypes_Icons_IconId",
                table: "IssueTypes");

            migrationBuilder.DropIndex(
                name: "IX_IssueTypes_ColorId",
                table: "IssueTypes");

            migrationBuilder.DropColumn(
                name: "ColorId",
                table: "IssueTypes");

            migrationBuilder.RenameColumn(
                name: "IconId",
                table: "IssueTypes",
                newName: "ColorIconId");

            migrationBuilder.RenameIndex(
                name: "IX_IssueTypes_IconId",
                table: "IssueTypes",
                newName: "IX_IssueTypes_ColorIconId");

            migrationBuilder.CreateTable(
                name: "ColorIcons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ColorId = table.Column<int>(type: "int", nullable: false),
                    IconId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColorIcons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ColorIcons_Colors_ColorId",
                        column: x => x.ColorId,
                        principalTable: "Colors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ColorIcons_Icons_IconId",
                        column: x => x.IconId,
                        principalTable: "Icons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ColorIcons",
                columns: new[] { "Id", "ColorId", "IconId" },
                values: new object[] { 1, 34, 29 });

            migrationBuilder.InsertData(
                table: "ColorIcons",
                columns: new[] { "Id", "ColorId", "IconId" },
                values: new object[] { 2, 19, 27 });

            migrationBuilder.UpdateData(
                table: "IssueTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "ColorIconId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "IssueTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "ColorIconId",
                value: 2);

            migrationBuilder.CreateIndex(
                name: "IX_ColorIcons_ColorId",
                table: "ColorIcons",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_ColorIcons_IconId",
                table: "ColorIcons",
                column: "IconId");

            migrationBuilder.AddForeignKey(
                name: "FK_IssueTypes_ColorIcons_ColorIconId",
                table: "IssueTypes",
                column: "ColorIconId",
                principalTable: "ColorIcons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
