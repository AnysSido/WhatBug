using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class RestructureRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PermissionSchemeProjectRolePermission_ProjectRoles_ProjectRoleId",
                table: "PermissionSchemeProjectRolePermission");

            migrationBuilder.DropTable(
                name: "ProjectRoleUsers");

            migrationBuilder.DropTable(
                name: "ProjectUserProjectRole");

            migrationBuilder.DropTable(
                name: "ProjectRoles");

            migrationBuilder.RenameColumn(
                name: "ProjectRoleId",
                table: "PermissionSchemeProjectRolePermission",
                newName: "RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_PermissionSchemeProjectRolePermission_ProjectRoleId",
                table: "PermissionSchemeProjectRolePermission",
                newName: "IX_PermissionSchemeProjectRolePermission_RoleId");

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProjectRoleUser",
                columns: table => new
                {
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectRoleUser", x => new { x.ProjectId, x.RoleId, x.UserId });
                    table.ForeignKey(
                        name: "FK_ProjectRoleUser_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectRoleUser_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectRoleUser_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectRoleUser_RoleId",
                table: "ProjectRoleUser",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectRoleUser_UserId",
                table: "ProjectRoleUser",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PermissionSchemeProjectRolePermission_Roles_RoleId",
                table: "PermissionSchemeProjectRolePermission",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PermissionSchemeProjectRolePermission_Roles_RoleId",
                table: "PermissionSchemeProjectRolePermission");

            migrationBuilder.DropTable(
                name: "ProjectRoleUser");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "PermissionSchemeProjectRolePermission",
                newName: "ProjectRoleId");

            migrationBuilder.RenameIndex(
                name: "IX_PermissionSchemeProjectRolePermission_RoleId",
                table: "PermissionSchemeProjectRolePermission",
                newName: "IX_PermissionSchemeProjectRolePermission_ProjectRoleId");

            migrationBuilder.CreateTable(
                name: "ProjectRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProjectRoleUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectRoleUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectRoleUsers_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectRoleUsers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectUserProjectRole",
                columns: table => new
                {
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ProjectRoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectUserProjectRole", x => new { x.ProjectId, x.UserId, x.ProjectRoleId });
                    table.ForeignKey(
                        name: "FK_ProjectUserProjectRole_ProjectRoles_ProjectRoleId",
                        column: x => x.ProjectRoleId,
                        principalTable: "ProjectRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectUserProjectRole_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectUserProjectRole_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectRoleUsers_ProjectId",
                table: "ProjectRoleUsers",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectRoleUsers_UserId",
                table: "ProjectRoleUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectUserProjectRole_ProjectRoleId",
                table: "ProjectUserProjectRole",
                column: "ProjectRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectUserProjectRole_UserId",
                table: "ProjectUserProjectRole",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PermissionSchemeProjectRolePermission_ProjectRoles_ProjectRoleId",
                table: "PermissionSchemeProjectRolePermission",
                column: "ProjectRoleId",
                principalTable: "ProjectRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
