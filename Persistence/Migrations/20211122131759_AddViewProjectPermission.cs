using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WhatBug.Persistence.Migrations
{
    public partial class AddViewProjectPermission : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "Description", "Name", "Type" },
                values: new object[] { 14, "View a project and its issues.", "View Project", "Project" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 14);
        }
    }
}
