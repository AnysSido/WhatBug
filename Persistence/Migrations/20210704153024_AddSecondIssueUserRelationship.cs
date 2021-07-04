using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class AddSecondIssueUserRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Users_AssignedToId",
                table: "Issues");

            migrationBuilder.RenameColumn(
                name: "AssignedToId",
                table: "Issues",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Issues_AssignedToId",
                table: "Issues",
                newName: "IX_Issues_UserId");

            migrationBuilder.AddColumn<int>(
                name: "AssigneeId",
                table: "Issues",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReporterId",
                table: "Issues",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Issues_AssigneeId",
                table: "Issues",
                column: "AssigneeId");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_ReporterId",
                table: "Issues",
                column: "ReporterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Users_AssigneeId",
                table: "Issues",
                column: "AssigneeId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Users_ReporterId",
                table: "Issues",
                column: "ReporterId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Users_UserId",
                table: "Issues",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Users_AssigneeId",
                table: "Issues");

            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Users_ReporterId",
                table: "Issues");

            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Users_UserId",
                table: "Issues");

            migrationBuilder.DropIndex(
                name: "IX_Issues_AssigneeId",
                table: "Issues");

            migrationBuilder.DropIndex(
                name: "IX_Issues_ReporterId",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "AssigneeId",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "ReporterId",
                table: "Issues");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Issues",
                newName: "AssignedToId");

            migrationBuilder.RenameIndex(
                name: "IX_Issues_UserId",
                table: "Issues",
                newName: "IX_Issues_AssignedToId");

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Users_AssignedToId",
                table: "Issues",
                column: "AssignedToId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
