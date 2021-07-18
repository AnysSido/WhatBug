using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class RemoveColorAndIconFromPrio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Priorities_Icons_IconId",
                table: "Priorities");

            migrationBuilder.DropIndex(
                name: "IX_Priorities_IconId",
                table: "Priorities");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "Priorities");

            migrationBuilder.DropColumn(
                name: "IconId",
                table: "Priorities");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Priorities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IconId",
                table: "Priorities",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Priorities_IconId",
                table: "Priorities",
                column: "IconId");

            migrationBuilder.AddForeignKey(
                name: "FK_Priorities_Icons_IconId",
                table: "Priorities",
                column: "IconId",
                principalTable: "Icons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
