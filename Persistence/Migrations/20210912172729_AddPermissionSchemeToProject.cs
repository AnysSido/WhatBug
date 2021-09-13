using Microsoft.EntityFrameworkCore.Migrations;

namespace WhatBug.Persistence.Migrations
{
    public partial class AddPermissionSchemeToProject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PermissionSchemeId",
                table: "Projects",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_PermissionSchemeId",
                table: "Projects",
                column: "PermissionSchemeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_PermissionSchemes_PermissionSchemeId",
                table: "Projects",
                column: "PermissionSchemeId",
                principalTable: "PermissionSchemes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_PermissionSchemes_PermissionSchemeId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_PermissionSchemeId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "PermissionSchemeId",
                table: "Projects");
        }
    }
}
