using Microsoft.EntityFrameworkCore.Migrations;

namespace WhatBug.Persistence.Migrations
{
    public partial class DefinePermissionSchemeRolePermissions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PermissionSchemeRolePermission");

            migrationBuilder.CreateTable(
                name: "PermissionSchemeRolePermissions",
                columns: table => new
                {
                    PermissionSchemeId = table.Column<int>(type: "integer", nullable: false),
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    PermissionId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionSchemeRolePermissions", x => new { x.PermissionSchemeId, x.RoleId, x.PermissionId });
                    table.ForeignKey(
                        name: "FK_PermissionSchemeRolePermissions_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PermissionSchemeRolePermissions_PermissionSchemes_Permissio~",
                        column: x => x.PermissionSchemeId,
                        principalTable: "PermissionSchemes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PermissionSchemeRolePermissions_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PermissionSchemeRolePermissions_PermissionId",
                table: "PermissionSchemeRolePermissions",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionSchemeRolePermissions_RoleId",
                table: "PermissionSchemeRolePermissions",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PermissionSchemeRolePermissions");

            migrationBuilder.CreateTable(
                name: "PermissionSchemeRolePermission",
                columns: table => new
                {
                    PermissionSchemeId = table.Column<int>(type: "integer", nullable: false),
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    PermissionId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionSchemeRolePermission", x => new { x.PermissionSchemeId, x.RoleId, x.PermissionId });
                    table.ForeignKey(
                        name: "FK_PermissionSchemeRolePermission_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PermissionSchemeRolePermission_PermissionSchemes_Permission~",
                        column: x => x.PermissionSchemeId,
                        principalTable: "PermissionSchemes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PermissionSchemeRolePermission_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PermissionSchemeRolePermission_PermissionId",
                table: "PermissionSchemeRolePermission",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionSchemeRolePermission_RoleId",
                table: "PermissionSchemeRolePermission",
                column: "RoleId");
        }
    }
}
