using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class AddPermissionSchemeProjectRolePermission : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PermissionSchemeProjectRolePermission",
                columns: table => new
                {
                    PermissionSchemeId = table.Column<int>(type: "int", nullable: false),
                    ProjectRoleId = table.Column<int>(type: "int", nullable: false),
                    PermissionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionSchemeProjectRolePermission", x => new { x.PermissionSchemeId, x.ProjectRoleId, x.PermissionId });
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
                        name: "FK_PermissionSchemeProjectRolePermission_ProjectRoles_ProjectRoleId",
                        column: x => x.ProjectRoleId,
                        principalTable: "ProjectRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PermissionSchemeProjectRolePermission_PermissionId",
                table: "PermissionSchemeProjectRolePermission",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionSchemeProjectRolePermission_ProjectRoleId",
                table: "PermissionSchemeProjectRolePermission",
                column: "ProjectRoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PermissionSchemeProjectRolePermission");
        }
    }
}
