using Microsoft.EntityFrameworkCore.Migrations;

namespace WhatBug.Persistence.Migrations
{
    public partial class AddManagePrioritiesPermission : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "Description", "Name", "Type" },
                values: new object[] { 10, "Create, edit and delete priorities.", "Manage Priorities", "Global" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 10);
        }
    }
}
