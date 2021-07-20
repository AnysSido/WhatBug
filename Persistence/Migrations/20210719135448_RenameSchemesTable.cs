using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class RenameSchemesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RolePermissions_Schemes_SchemeId",
                table: "RolePermissions");

            migrationBuilder.DropTable(
                name: "Schemes");

            migrationBuilder.DropIndex(
                name: "IX_RolePermissions_SchemeId",
                table: "RolePermissions");

            migrationBuilder.AddColumn<int>(
                name: "PermissionSchemeId",
                table: "RolePermissions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PermissionSchemes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionSchemes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_PermissionSchemeId",
                table: "RolePermissions",
                column: "PermissionSchemeId");

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermissions_PermissionSchemes_PermissionSchemeId",
                table: "RolePermissions",
                column: "PermissionSchemeId",
                principalTable: "PermissionSchemes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RolePermissions_PermissionSchemes_PermissionSchemeId",
                table: "RolePermissions");

            migrationBuilder.DropTable(
                name: "PermissionSchemes");

            migrationBuilder.DropIndex(
                name: "IX_RolePermissions_PermissionSchemeId",
                table: "RolePermissions");

            migrationBuilder.DropColumn(
                name: "PermissionSchemeId",
                table: "RolePermissions");

            migrationBuilder.CreateTable(
                name: "Schemes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schemes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_SchemeId",
                table: "RolePermissions",
                column: "SchemeId");

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermissions_Schemes_SchemeId",
                table: "RolePermissions",
                column: "SchemeId",
                principalTable: "Schemes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
