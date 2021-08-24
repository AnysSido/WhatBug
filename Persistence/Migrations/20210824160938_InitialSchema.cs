using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace WhatBug.Persistence.Migrations
{
    public partial class InitialSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Colors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Icons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Icons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IssueStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssueStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Type = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PermissionSchemes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<int>(type: "integer", nullable: false),
                    LastModified = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionSchemes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PrioritySchemes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrioritySchemes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Username = table.Column<string>(type: "text", nullable: true),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    Surname = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    JobTitle = table.Column<string>(type: "text", nullable: true),
                    Department = table.Column<string>(type: "text", nullable: true),
                    Organization = table.Column<string>(type: "text", nullable: true),
                    Location = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IssueTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    IconId = table.Column<int>(type: "integer", nullable: false),
                    ColorId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssueTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IssueTypes_Colors_ColorId",
                        column: x => x.ColorId,
                        principalTable: "Colors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IssueTypes_Icons_IconId",
                        column: x => x.IconId,
                        principalTable: "Icons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Priorities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Order = table.Column<int>(type: "integer", nullable: false),
                    ColorId = table.Column<int>(type: "integer", nullable: false),
                    IconId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Priorities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Priorities_Colors_ColorId",
                        column: x => x.ColorId,
                        principalTable: "Colors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Priorities_Icons_IconId",
                        column: x => x.IconId,
                        principalTable: "Icons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Key = table.Column<string>(type: "text", nullable: false),
                    IssueCounter = table.Column<int>(type: "integer", nullable: false),
                    PrioritySchemeId = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<int>(type: "integer", nullable: false),
                    LastModified = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projects_PrioritySchemes_PrioritySchemeId",
                        column: x => x.PrioritySchemeId,
                        principalTable: "PrioritySchemes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateTable(
                name: "UserPermission",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    PermissionId = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<int>(type: "integer", nullable: false),
                    LastModified = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPermission", x => new { x.UserId, x.PermissionId });
                    table.ForeignKey(
                        name: "FK_UserPermission_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserPermission_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PriorityPriorityScheme",
                columns: table => new
                {
                    PrioritiesId = table.Column<int>(type: "integer", nullable: false),
                    PrioritySchemesId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriorityPriorityScheme", x => new { x.PrioritiesId, x.PrioritySchemesId });
                    table.ForeignKey(
                        name: "FK_PriorityPriorityScheme_Priorities_PrioritiesId",
                        column: x => x.PrioritiesId,
                        principalTable: "Priorities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PriorityPriorityScheme_PrioritySchemes_PrioritySchemesId",
                        column: x => x.PrioritySchemesId,
                        principalTable: "PrioritySchemes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Issues",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Summary = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    ProjectId = table.Column<int>(type: "integer", nullable: false),
                    ReporterId = table.Column<int>(type: "integer", nullable: false),
                    AssigneeId = table.Column<int>(type: "integer", nullable: true),
                    IssueTypeId = table.Column<int>(type: "integer", nullable: false),
                    IssueStatusId = table.Column<int>(type: "integer", nullable: false),
                    PriorityId = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<int>(type: "integer", nullable: false),
                    LastModified = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Issues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Issues_IssueStatuses_IssueStatusId",
                        column: x => x.IssueStatusId,
                        principalTable: "IssueStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Issues_IssueTypes_IssueTypeId",
                        column: x => x.IssueTypeId,
                        principalTable: "IssueTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Issues_Priorities_PriorityId",
                        column: x => x.PriorityId,
                        principalTable: "Priorities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Issues_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Issues_Users_AssigneeId",
                        column: x => x.AssigneeId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Issues_Users_ReporterId",
                        column: x => x.ReporterId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectRoleUser",
                columns: table => new
                {
                    ProjectId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    RoleId = table.Column<int>(type: "integer", nullable: false)
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

            migrationBuilder.InsertData(
                table: "Colors",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "White" },
                    { 35, "Cobalt" },
                    { 36, "Navy" },
                    { 37, "Mint" },
                    { 38, "Seafoam" },
                    { 39, "Lime" },
                    { 40, "Olive" },
                    { 41, "Pear" },
                    { 42, "Pickle" },
                    { 43, "Green" },
                    { 44, "Shamrock" },
                    { 45, "Emerald" },
                    { 46, "Basil" },
                    { 47, "Tortilla" },
                    { 34, "Blue" },
                    { 48, "Peanut" },
                    { 50, "Caramel" },
                    { 51, "Coffee" },
                    { 52, "Mocha" },
                    { 53, "Hickory" },
                    { 55, "Cloud" },
                    { 56, "Silver" },
                    { 57, "Coin" },
                    { 58, "Ash" },
                    { 59, "Gray" },
                    { 60, "Pewter" },
                    { 61, "Smoke" },
                    { 62, "Slate" },
                    { 63, "Black" },
                    { 49, "Tawny" },
                    { 33, "Cerulean" },
                    { 54, "Brown" },
                    { 31, "Sky" },
                    { 32, "Sapphire" },
                    { 2, "Chiffon" },
                    { 3, "Frost" },
                    { 4, "Tan" },
                    { 5, "Buttermilk" },
                    { 6, "Beige" },
                    { 7, "Oat" },
                    { 8, "Cookie" },
                    { 10, "Lemon" },
                    { 11, "Yellow" },
                    { 12, "Honey" },
                    { 13, "Gold" },
                    { 14, "Apricot" },
                    { 15, "Orange" },
                    { 9, "Latte" },
                    { 17, "Red" },
                    { 16, "Blush" },
                    { 29, "Purple" },
                    { 28, "Amethyst" },
                    { 27, "Iris" },
                    { 26, "Lilac" },
                    { 24, "Hotpink" },
                    { 23, "Fushcia" },
                    { 25, "Magenta" },
                    { 22, "Pink" },
                    { 21, "Cherry" },
                    { 20, "Apple" },
                    { 19, "Candy" },
                    { 18, "Rose" },
                    { 30, "Violet" }
                });

            migrationBuilder.InsertData(
                table: "Icons",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 26, "Equals" },
                    { 20, "AnglesRight" },
                    { 22, "CircleExclaimation" },
                    { 23, "TriangleExclaimation" },
                    { 24, "XMark" },
                    { 25, "Ban" },
                    { 21, "Exclaimation" },
                    { 32, "CaretSquareLeft" },
                    { 28, "PlusSquare" },
                    { 29, "CheckSquare" },
                    { 30, "CaretSquareUp" },
                    { 31, "CaretSquareDown" },
                    { 19, "AnglesLeft" },
                    { 33, "CaretSquareRight" },
                    { 27, "Bug" },
                    { 18, "AnglesDown" },
                    { 7, "CircleArrowLeft" },
                    { 16, "AngleRight" },
                    { 1, "ArrowUp" },
                    { 17, "AnglesUp" },
                    { 3, "ArrowLeft" },
                    { 4, "ArrowRight" },
                    { 5, "CircleArrowUp" },
                    { 6, "CircleArrowDown" },
                    { 8, "CircleArrowRight" },
                    { 2, "ArrowDown" },
                    { 10, "ChevronDown" },
                    { 11, "ChevronLeft" },
                    { 12, "ChevronRight" },
                    { 13, "AngleUp" },
                    { 14, "AngleDown" },
                    { 15, "AngleLeft" },
                    { 9, "ChevronUp" }
                });

            migrationBuilder.InsertData(
                table: "IssueStatuses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 4, "Done" },
                    { 3, "In Progress" },
                    { 1, "Backlog" },
                    { 2, "ToDo" }
                });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "Description", "Name", "Type" },
                values: new object[,]
                {
                    { 7, "Delete issues within a project.", "Delete Issue", "Project" },
                    { 1, "Create new projects.", "Create Project", "Global" },
                    { 2, "Delete existing projects.", "Delete Project", "Global" },
                    { 3, "Edit global permissions assigned to users.", "Edit User Permissions", "Global" },
                    { 4, "View all projects in WhatBug. Users without this permission must be a member of a project to view it.", "View All Projects", "Global" },
                    { 5, "Create new issues within a project.", "Create Issue", "Project" },
                    { 6, "Edit existing issues within a project.", "Edit Issue", "Project" }
                });

            migrationBuilder.InsertData(
                table: "PrioritySchemes",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 1, "The default priority scheme used by all projects without any other scheme assigned.", "Default" });

            migrationBuilder.InsertData(
                table: "IssueTypes",
                columns: new[] { "Id", "ColorId", "IconId", "Name" },
                values: new object[,]
                {
                    { 2, 19, 27, "Bug" },
                    { 1, 34, 29, "Task" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Issues_AssigneeId",
                table: "Issues",
                column: "AssigneeId");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_IssueStatusId",
                table: "Issues",
                column: "IssueStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_IssueTypeId",
                table: "Issues",
                column: "IssueTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_PriorityId",
                table: "Issues",
                column: "PriorityId");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_ProjectId",
                table: "Issues",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_ReporterId",
                table: "Issues",
                column: "ReporterId");

            migrationBuilder.CreateIndex(
                name: "IX_IssueTypes_ColorId",
                table: "IssueTypes",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_IssueTypes_IconId",
                table: "IssueTypes",
                column: "IconId");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionSchemeRolePermission_PermissionId",
                table: "PermissionSchemeRolePermission",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionSchemeRolePermission_RoleId",
                table: "PermissionSchemeRolePermission",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Priorities_ColorId",
                table: "Priorities",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_Priorities_IconId",
                table: "Priorities",
                column: "IconId");

            migrationBuilder.CreateIndex(
                name: "IX_PriorityPriorityScheme_PrioritySchemesId",
                table: "PriorityPriorityScheme",
                column: "PrioritySchemesId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectRoleUser_RoleId",
                table: "ProjectRoleUser",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectRoleUser_UserId",
                table: "ProjectRoleUser",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_PrioritySchemeId",
                table: "Projects",
                column: "PrioritySchemeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPermission_PermissionId",
                table: "UserPermission",
                column: "PermissionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Issues");

            migrationBuilder.DropTable(
                name: "PermissionSchemeRolePermission");

            migrationBuilder.DropTable(
                name: "PriorityPriorityScheme");

            migrationBuilder.DropTable(
                name: "ProjectRoleUser");

            migrationBuilder.DropTable(
                name: "UserPermission");

            migrationBuilder.DropTable(
                name: "IssueStatuses");

            migrationBuilder.DropTable(
                name: "IssueTypes");

            migrationBuilder.DropTable(
                name: "PermissionSchemes");

            migrationBuilder.DropTable(
                name: "Priorities");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Colors");

            migrationBuilder.DropTable(
                name: "Icons");

            migrationBuilder.DropTable(
                name: "PrioritySchemes");
        }
    }
}
