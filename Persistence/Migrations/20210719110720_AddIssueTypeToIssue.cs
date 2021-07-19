using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class AddIssueTypeToIssue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IssueTypeId",
                table: "Issues",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Issues_IssueTypeId",
                table: "Issues",
                column: "IssueTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_IssueTypes_IssueTypeId",
                table: "Issues",
                column: "IssueTypeId",
                principalTable: "IssueTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issues_IssueTypes_IssueTypeId",
                table: "Issues");

            migrationBuilder.DropIndex(
                name: "IX_Issues_IssueTypeId",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "IssueTypeId",
                table: "Issues");
        }
    }
}
