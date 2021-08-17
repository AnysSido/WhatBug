using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class ChangePriorityColorCols : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Priorities_ColorIcons_ColorIconId",
                table: "Priorities");

            migrationBuilder.RenameColumn(
                name: "ColorIconId",
                table: "Priorities",
                newName: "IconId");

            migrationBuilder.RenameIndex(
                name: "IX_Priorities_ColorIconId",
                table: "Priorities",
                newName: "IX_Priorities_IconId");

            migrationBuilder.AddColumn<int>(
                name: "ColorId",
                table: "Priorities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Priorities_ColorId",
                table: "Priorities",
                column: "ColorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Priorities_Colors_ColorId",
                table: "Priorities",
                column: "ColorId",
                principalTable: "Colors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_Priorities_Colors_ColorId",
                table: "Priorities");

            migrationBuilder.DropForeignKey(
                name: "FK_Priorities_Icons_IconId",
                table: "Priorities");

            migrationBuilder.DropIndex(
                name: "IX_Priorities_ColorId",
                table: "Priorities");

            migrationBuilder.DropColumn(
                name: "ColorId",
                table: "Priorities");

            migrationBuilder.RenameColumn(
                name: "IconId",
                table: "Priorities",
                newName: "ColorIconId");

            migrationBuilder.RenameIndex(
                name: "IX_Priorities_IconId",
                table: "Priorities",
                newName: "IX_Priorities_ColorIconId");

            migrationBuilder.AddForeignKey(
                name: "FK_Priorities_ColorIcons_ColorIconId",
                table: "Priorities",
                column: "ColorIconId",
                principalTable: "ColorIcons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
