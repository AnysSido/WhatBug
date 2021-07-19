using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class UpdatePriorityIcon : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Priorities_Icons_IconId",
                table: "Priorities");

            migrationBuilder.AlterColumn<int>(
                name: "IconId",
                table: "Priorities",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ColorIconId",
                table: "Priorities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Priorities_ColorIconId",
                table: "Priorities",
                column: "ColorIconId");

            migrationBuilder.AddForeignKey(
                name: "FK_Priorities_ColorIcons_ColorIconId",
                table: "Priorities",
                column: "ColorIconId",
                principalTable: "ColorIcons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Priorities_Icons_IconId",
                table: "Priorities",
                column: "IconId",
                principalTable: "Icons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Priorities_ColorIcons_ColorIconId",
                table: "Priorities");

            migrationBuilder.DropForeignKey(
                name: "FK_Priorities_Icons_IconId",
                table: "Priorities");

            migrationBuilder.DropIndex(
                name: "IX_Priorities_ColorIconId",
                table: "Priorities");

            migrationBuilder.DropColumn(
                name: "ColorIconId",
                table: "Priorities");

            migrationBuilder.AlterColumn<int>(
                name: "IconId",
                table: "Priorities",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Priorities_Icons_IconId",
                table: "Priorities",
                column: "IconId",
                principalTable: "Icons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
