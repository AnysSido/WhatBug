using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WhatBug.Persistence.Migrations
{
    public partial class AddRoleAssignPermissions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "Description", "Name", "Type" },
                values: new object[,]
                {
                    { 12, "Assign users to roles within a project.", "Assign User Roles", "Project" },
                    { 13, "View project members and their roles.", "View Project Members", "Project" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 13);
        }
    }
}
