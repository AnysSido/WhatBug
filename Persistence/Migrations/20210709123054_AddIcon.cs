using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class AddIcon : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "PriorityIcons",
                columns: new[] { "Id", "Name" },
                values: new object[] { 26, "Equals" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PriorityIcons",
                keyColumn: "Id",
                keyValue: 26);
        }
    }
}
