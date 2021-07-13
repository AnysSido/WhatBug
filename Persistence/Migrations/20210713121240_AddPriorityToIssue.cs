using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class AddPriorityToIssue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PriorityId",
                table: "Issues",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Issues_PriorityId",
                table: "Issues",
                column: "PriorityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Priorities_PriorityId",
                table: "Issues",
                column: "PriorityId",
                principalTable: "Priorities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Priorities_PriorityId",
                table: "Issues");

            migrationBuilder.DropIndex(
                name: "IX_Issues_PriorityId",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "PriorityId",
                table: "Issues");
        }
    }
}
