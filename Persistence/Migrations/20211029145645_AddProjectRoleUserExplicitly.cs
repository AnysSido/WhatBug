using Microsoft.EntityFrameworkCore.Migrations;

namespace WhatBug.Persistence.Migrations
{
    public partial class AddProjectRoleUserExplicitly : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectRoleUser_Projects_ProjectId",
                table: "ProjectRoleUser");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectRoleUser_Roles_RoleId",
                table: "ProjectRoleUser");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectRoleUser_Users_UserId",
                table: "ProjectRoleUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectRoleUser",
                table: "ProjectRoleUser");

            migrationBuilder.RenameTable(
                name: "ProjectRoleUser",
                newName: "ProjectRoleUsers");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectRoleUser_UserId",
                table: "ProjectRoleUsers",
                newName: "IX_ProjectRoleUsers_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectRoleUser_RoleId",
                table: "ProjectRoleUsers",
                newName: "IX_ProjectRoleUsers_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectRoleUsers",
                table: "ProjectRoleUsers",
                columns: new[] { "ProjectId", "RoleId", "UserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectRoleUsers_Projects_ProjectId",
                table: "ProjectRoleUsers",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectRoleUsers_Roles_RoleId",
                table: "ProjectRoleUsers",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectRoleUsers_Users_UserId",
                table: "ProjectRoleUsers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectRoleUsers_Projects_ProjectId",
                table: "ProjectRoleUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectRoleUsers_Roles_RoleId",
                table: "ProjectRoleUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectRoleUsers_Users_UserId",
                table: "ProjectRoleUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectRoleUsers",
                table: "ProjectRoleUsers");

            migrationBuilder.RenameTable(
                name: "ProjectRoleUsers",
                newName: "ProjectRoleUser");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectRoleUsers_UserId",
                table: "ProjectRoleUser",
                newName: "IX_ProjectRoleUser_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectRoleUsers_RoleId",
                table: "ProjectRoleUser",
                newName: "IX_ProjectRoleUser_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectRoleUser",
                table: "ProjectRoleUser",
                columns: new[] { "ProjectId", "RoleId", "UserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectRoleUser_Projects_ProjectId",
                table: "ProjectRoleUser",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectRoleUser_Roles_RoleId",
                table: "ProjectRoleUser",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectRoleUser_Users_UserId",
                table: "ProjectRoleUser",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
