using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class AddIssueStatusToIssues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IssueStatusId",
                table: "Issues",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Issues_IssueStatusId",
                table: "Issues",
                column: "IssueStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_IssueStatuses_IssueStatusId",
                table: "Issues",
                column: "IssueStatusId",
                principalTable: "IssueStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issues_IssueStatuses_IssueStatusId",
                table: "Issues");

            migrationBuilder.DropIndex(
                name: "IX_Issues_IssueStatusId",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "IssueStatusId",
                table: "Issues");
        }
    }
}
