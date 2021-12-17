using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WhatBug.Persistence.Migrations
{
    public partial class AddAttachmentPermission : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "Description", "Name", "Type" },
                values: new object[] { 17, "Attach files to issues.", "Attach Files", "Project" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 17);
        }
    }
}
