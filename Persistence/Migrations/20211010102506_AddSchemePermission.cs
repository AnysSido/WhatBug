using Microsoft.EntityFrameworkCore.Migrations;

namespace WhatBug.Persistence.Migrations
{
    public partial class AddSchemePermission : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Grant and/or deny user-level permissions.", "Manage User Permissions" });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "Description", "Name", "Type" },
                values: new object[] { 9, "Create, edit and delete permission schemes.", "Manage Permission Schemes", "Global" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Edit global permissions assigned to users.", "Edit User Permissions" });
        }
    }
}
