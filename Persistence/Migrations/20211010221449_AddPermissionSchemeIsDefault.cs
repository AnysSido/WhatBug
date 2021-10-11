using Microsoft.EntityFrameworkCore.Migrations;

namespace WhatBug.Persistence.Migrations
{
    public partial class AddPermissionSchemeIsDefault : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                table: "PermissionSchemes",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "PermissionSchemes",
                keyColumn: "Id",
                keyValue: 1,
                column: "IsDefault",
                value: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDefault",
                table: "PermissionSchemes");
        }
    }
}
