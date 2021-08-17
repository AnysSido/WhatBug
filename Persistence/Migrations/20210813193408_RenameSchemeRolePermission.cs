using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class RenameSchemeRolePermission : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PermissionSchemeProjectRolePermission");

            migrationBuilder.CreateTable(
                name: "PermissionSchemeRolePermission",
                columns: table => new
                {
                    PermissionSchemeId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    PermissionId = table.Column<int>(type: "int", nullable: false)
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
                        name: "FK_PermissionSchemeRolePermission_PermissionSchemes_PermissionSchemeId",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PermissionSchemeRolePermission");

            migrationBuilder.CreateTable(
                name: "PermissionSchemeProjectRolePermission",
                columns: table => new
                {
                    PermissionSchemeId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    PermissionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionSchemeProjectRolePermission", x => new { x.PermissionSchemeId, x.RoleId, x.PermissionId });
                    table.ForeignKey(
                        name: "FK_PermissionSchemeProjectRolePermission_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PermissionSchemeProjectRolePermission_PermissionSchemes_PermissionSchemeId",
                        column: x => x.PermissionSchemeId,
                        principalTable: "PermissionSchemes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PermissionSchemeProjectRolePermission_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PermissionSchemeProjectRolePermission_PermissionId",
                table: "PermissionSchemeProjectRolePermission",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionSchemeProjectRolePermission_RoleId",
                table: "PermissionSchemeProjectRolePermission",
                column: "RoleId");
        }
    }
}
