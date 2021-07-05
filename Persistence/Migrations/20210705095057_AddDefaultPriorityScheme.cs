using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class AddDefaultPriorityScheme : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "PrioritySchemes",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 1, "The default priority scheme used by all projects without any other scheme assigned.", "Default" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PrioritySchemes",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
