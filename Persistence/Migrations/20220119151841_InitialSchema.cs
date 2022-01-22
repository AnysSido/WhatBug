using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

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
                    Name = table.Column<string>(type: "text", nullable: true),
                    WebName = table.Column<string>(type: "text", nullable: true)
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
                    IsDefault = table.Column<bool>(type: "boolean", nullable: false),
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
                    Description = table.Column<string>(type: "text", nullable: true),
                    IsDefault = table.Column<bool>(type: "boolean", nullable: false)
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
                    IsDefault = table.Column<bool>(type: "boolean", nullable: false),
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
                    PermissionSchemeId = table.Column<int>(type: "integer", nullable: true),
                    CreatedBy = table.Column<int>(type: "integer", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<int>(type: "integer", nullable: false),
                    LastModified = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projects_PermissionSchemes_PermissionSchemeId",
                        column: x => x.PermissionSchemeId,
                        principalTable: "PermissionSchemes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Projects_PrioritySchemes_PrioritySchemeId",
                        column: x => x.PrioritySchemeId,
                        principalTable: "PrioritySchemes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateTable(
                name: "UserPermissions",
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
                    table.PrimaryKey("PK_UserPermissions", x => new { x.UserId, x.PermissionId });
                    table.ForeignKey(
                        name: "FK_UserPermissions_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserPermissions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrioritySchemePriorities",
                columns: table => new
                {
                    PrioritySchemeId = table.Column<int>(type: "integer", nullable: false),
                    PriorityId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrioritySchemePriorities", x => new { x.PrioritySchemeId, x.PriorityId });
                    table.ForeignKey(
                        name: "FK_PrioritySchemePriorities_Priorities_PriorityId",
                        column: x => x.PriorityId,
                        principalTable: "Priorities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PrioritySchemePriorities_PrioritySchemes_PrioritySchemeId",
                        column: x => x.PrioritySchemeId,
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
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Issues_Users_ReporterId",
                        column: x => x.ReporterId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectRoleUsers",
                columns: table => new
                {
                    ProjectId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    RoleId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectRoleUsers", x => new { x.ProjectId, x.RoleId, x.UserId });
                    table.ForeignKey(
                        name: "FK_ProjectRoleUsers_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectRoleUsers_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
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
                name: "Attachments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FileName = table.Column<string>(type: "text", nullable: true),
                    OriginalFileName = table.Column<string>(type: "text", nullable: true),
                    FileSize = table.Column<long>(type: "bigint", nullable: false),
                    ContentType = table.Column<string>(type: "text", nullable: true),
                    IssueId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attachments_Issues_IssueId",
                        column: x => x.IssueId,
                        principalTable: "Issues",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "IssueComments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Content = table.Column<string>(type: "text", nullable: true),
                    AuthorId = table.Column<int>(type: "integer", nullable: false),
                    IssueId = table.Column<string>(type: "text", nullable: true),
                    Timestamp = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssueComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IssueComments_Issues_IssueId",
                        column: x => x.IssueId,
                        principalTable: "Issues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_IssueComments_Users_AuthorId",
                        column: x => x.AuthorId,
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
                    { 2, "Chiffon" },
                    { 3, "Frost" },
                    { 4, "Tan" },
                    { 5, "Buttermilk" },
                    { 6, "Beige" },
                    { 7, "Oat" },
                    { 8, "Cookie" },
                    { 9, "Latte" },
                    { 10, "Lemon" },
                    { 11, "Yellow" },
                    { 12, "Honey" },
                    { 13, "Gold" },
                    { 14, "Apricot" },
                    { 15, "Orange" },
                    { 16, "Blush" },
                    { 17, "Red" },
                    { 18, "Rose" },
                    { 19, "Candy" },
                    { 20, "Apple" },
                    { 21, "Cherry" },
                    { 22, "Pink" },
                    { 23, "Fushcia" },
                    { 24, "Hotpink" },
                    { 25, "Magenta" },
                    { 26, "Lilac" },
                    { 27, "Iris" },
                    { 28, "Amethyst" },
                    { 29, "Purple" },
                    { 30, "Violet" },
                    { 31, "Sky" },
                    { 32, "Sapphire" },
                    { 33, "Cerulean" },
                    { 34, "Blue" },
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
                    { 48, "Peanut" },
                    { 49, "Tawny" },
                    { 50, "Caramel" },
                    { 51, "Coffee" },
                    { 52, "Mocha" },
                    { 53, "Hickory" },
                    { 54, "Brown" },
                    { 55, "Cloud" },
                    { 56, "Silver" },
                    { 57, "Coin" },
                    { 58, "Ash" },
                    { 59, "Gray" },
                    { 60, "Pewter" },
                    { 61, "Smoke" },
                    { 62, "Slate" },
                    { 63, "Black" }
                });

            migrationBuilder.InsertData(
                table: "Icons",
                columns: new[] { "Id", "Name", "WebName" },
                values: new object[,]
                {
                    { 1, "ArrowUp", "arrow-up" },
                    { 2, "ArrowDown", "arrow-down" },
                    { 3, "ArrowLeft", "arrow-left" },
                    { 4, "ArrowRight", "arrow-right" },
                    { 5, "CircleArrowUp", "arrow-circle-up" },
                    { 6, "CircleArrowDown", "arrow-circle-down" },
                    { 7, "CircleArrowLeft", "arrow-circle-left" },
                    { 8, "CircleArrowRight", "arrow-circle-right" },
                    { 9, "ChevronUp", "chevron-up" },
                    { 10, "ChevronDown", "chevron-down" },
                    { 11, "ChevronLeft", "chevron-left" },
                    { 12, "ChevronRight", "chevron-right" },
                    { 13, "AngleUp", "angle-up" },
                    { 14, "AngleDown", "angle-down" },
                    { 15, "AngleLeft", "angle-left" },
                    { 16, "AngleRight", "angle-right" },
                    { 17, "AnglesUp", "angles-up" },
                    { 18, "AnglesDown", "angles-down" },
                    { 19, "AnglesLeft", "angles-left" },
                    { 20, "AnglesRight", "angles-right" },
                    { 21, "Exclaimaton", "exclamation" },
                    { 22, "Circle Exclamation", "exclamation-circle" },
                    { 23, "Triangle Exclamation", "exclamation-triangle" },
                    { 24, "X Mark", "x-mark" },
                    { 25, "Ban", "ban" },
                    { 26, "Equals", "equals" },
                    { 27, "Bug", "bug" },
                    { 28, "Square Plus", "plus-square" },
                    { 29, "Square Check", "check-square" },
                    { 30, "Square Caret Up", "caret-up-square" },
                    { 31, "Square Caret Down", "caret-down-square" },
                    { 32, "Square Caret Left", "caret-left-square" },
                    { 33, "Square Caret Right", "caret-right-square" },
                    { 34, "Pen", "pen" },
                    { 35, "Plus", "plus" },
                    { 36, "Information", "information" },
                    { 37, "Circle", "circle" },
                    { 38, "Wave Square", "wave-square" }
                });

            migrationBuilder.InsertData(
                table: "IssueStatuses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Backlog" },
                    { 2, "To Do" },
                    { 3, "In Progress" },
                    { 4, "Done" }
                });

            migrationBuilder.InsertData(
                table: "PermissionSchemes",
                columns: new[] { "Id", "Created", "CreatedBy", "Description", "IsDefault", "LastModified", "LastModifiedBy", "Name" },
                values: new object[] { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "The default permission scheme used by all projects without any other scheme assigned.", true, null, 0, "Default" });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "Description", "Name", "Type" },
                values: new object[,]
                {
                    { 1, "Create new projects.", "Create Project", "Global" },
                    { 2, "Delete existing projects.", "Delete Project", "Global" },
                    { 3, "Grant and/or deny user-level permissions.", "Manage User Permissions", "Global" },
                    { 4, "View all projects in WhatBug. Users without this permission must be a member of a project to view it.", "View All Projects", "Global" },
                    { 5, "Create, edit and delete project roles used by permission schemes.", "Manage Project Roles", "Global" },
                    { 6, "Create new issues within a project.", "Create Issue", "Project" },
                    { 7, "Edit existing issues within a project.", "Edit Issue", "Project" },
                    { 8, "Delete issues within a project.", "Delete Issue", "Project" },
                    { 9, "Create, edit and delete permission schemes.", "Manage Permission Schemes", "Global" },
                    { 10, "Create, edit and delete priorities.", "Manage Priorities", "Global" },
                    { 11, "Create, edit and delete priority schemes.", "Manage Priority Schemes", "Global" },
                    { 12, "Assign users to roles within a project.", "Assign User Roles", "Project" },
                    { 13, "View project members and their roles.", "View Project Members", "Project" },
                    { 14, "View a project and its issues.", "View Project", "Project" },
                    { 15, "Change an issue status. For example dragging an issue on the kanban board.", "Set Issue Status", "Project" },
                    { 16, "Add comments to issues.", "Comment", "Project" },
                    { 17, "Attach files to issues.", "Attach Files", "Project" }
                });

            migrationBuilder.InsertData(
                table: "PrioritySchemes",
                columns: new[] { "Id", "Description", "IsDefault", "Name" },
                values: new object[] { 1, "The default priority scheme used by all projects without any other scheme assigned.", true, "Default" });

            migrationBuilder.InsertData(
                table: "IssueTypes",
                columns: new[] { "Id", "ColorId", "IconId", "Name" },
                values: new object[,]
                {
                    { 1, 34, 29, "Task" },
                    { 2, 19, 27, "Bug" }
                });

            migrationBuilder.InsertData(
                table: "Priorities",
                columns: new[] { "Id", "ColorId", "Description", "IconId", "IsDefault", "Name", "Order" },
                values: new object[] { 1, 63, "The default priority used by all issues without any other priority assigned.", 38, true, "Default", 0 });

            migrationBuilder.CreateIndex(
                name: "IX_Attachments_IssueId",
                table: "Attachments",
                column: "IssueId");

            migrationBuilder.CreateIndex(
                name: "IX_IssueComments_AuthorId",
                table: "IssueComments",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_IssueComments_IssueId",
                table: "IssueComments",
                column: "IssueId");

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
                name: "IX_PermissionSchemeRolePermissions_PermissionId",
                table: "PermissionSchemeRolePermissions",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionSchemeRolePermissions_RoleId",
                table: "PermissionSchemeRolePermissions",
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
                name: "IX_PrioritySchemePriorities_PriorityId",
                table: "PrioritySchemePriorities",
                column: "PriorityId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectRoleUsers_RoleId",
                table: "ProjectRoleUsers",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectRoleUsers_UserId",
                table: "ProjectRoleUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_PermissionSchemeId",
                table: "Projects",
                column: "PermissionSchemeId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_PrioritySchemeId",
                table: "Projects",
                column: "PrioritySchemeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPermissions_PermissionId",
                table: "UserPermissions",
                column: "PermissionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attachments");

            migrationBuilder.DropTable(
                name: "IssueComments");

            migrationBuilder.DropTable(
                name: "PermissionSchemeRolePermissions");

            migrationBuilder.DropTable(
                name: "PrioritySchemePriorities");

            migrationBuilder.DropTable(
                name: "ProjectRoleUsers");

            migrationBuilder.DropTable(
                name: "UserPermissions");

            migrationBuilder.DropTable(
                name: "Issues");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "IssueStatuses");

            migrationBuilder.DropTable(
                name: "IssueTypes");

            migrationBuilder.DropTable(
                name: "Priorities");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Colors");

            migrationBuilder.DropTable(
                name: "Icons");

            migrationBuilder.DropTable(
                name: "PermissionSchemes");

            migrationBuilder.DropTable(
                name: "PrioritySchemes");
        }
    }
}
