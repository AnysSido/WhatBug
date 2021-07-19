using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class AddColorIconAndIssueTypeSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IssueTypes_Icons_IconId",
                table: "IssueTypes");

            migrationBuilder.DeleteData(
                table: "IssueTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "IssueTypes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.RenameColumn(
                name: "IconId",
                table: "IssueTypes",
                newName: "ColorIconId");

            migrationBuilder.RenameIndex(
                name: "IX_IssueTypes_IconId",
                table: "IssueTypes",
                newName: "IX_IssueTypes_ColorIconId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_IssueTypes_ColorIcons_ColorIconId",
                table: "IssueTypes",
                column: "ColorIconId",
                principalTable: "ColorIcons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IssueTypes_ColorIcons_ColorIconId",
                table: "IssueTypes");

            migrationBuilder.DeleteData(
                table: "ColorIcons",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ColorIcons",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.RenameColumn(
                name: "ColorIconId",
                table: "IssueTypes",
                newName: "IconId");

            migrationBuilder.RenameIndex(
                name: "IX_IssueTypes_ColorIconId",
                table: "IssueTypes",
                newName: "IX_IssueTypes_IconId");

            migrationBuilder.UpdateData(
                table: "IssueTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "IconId",
                value: 29);

            migrationBuilder.UpdateData(
                table: "IssueTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "IconId",
                value: 27);

            migrationBuilder.InsertData(
                table: "IssueTypes",
                columns: new[] { "Id", "IconId", "Name" },
                values: new object[,]
                {
                    { 3, 28, "New Feature" },
                    { 4, 30, "Improvement" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_IssueTypes_Icons_IconId",
                table: "IssueTypes",
                column: "IconId",
                principalTable: "Icons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
