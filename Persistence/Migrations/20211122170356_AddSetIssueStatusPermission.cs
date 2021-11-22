using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WhatBug.Persistence.Migrations
{
    public partial class AddSetIssueStatusPermission : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "Description", "Name", "Type" },
                values: new object[] { 15, "Change an issue status. For example dragging an issue on the kanban board.", "Set Issue Status", "Project" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 15);
        }
    }
}
