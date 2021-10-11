using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WhatBug.Persistence.Migrations
{
    public partial class AddDefaultPermissionScheme : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "PermissionSchemes",
                columns: new[] { "Id", "Created", "CreatedBy", "Description", "LastModified", "LastModifiedBy", "Name" },
                values: new object[] { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "The default permission scheme used by all projects without any other scheme assigned.", null, 0, "Default" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PermissionSchemes",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
