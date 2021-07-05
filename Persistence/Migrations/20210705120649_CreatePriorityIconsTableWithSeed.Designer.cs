﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WhatBug.Persistence;

namespace Persistence.Migrations
{
    [DbContext(typeof(WhatBugDbContext))]
    [Migration("20210705120649_CreatePriorityIconsTableWithSeed")]
    partial class CreatePriorityIconsTableWithSeed
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PriorityPriorityScheme", b =>
                {
                    b.Property<int>("PrioritiesId")
                        .HasColumnType("int");

                    b.Property<int>("PrioritySchemesId")
                        .HasColumnType("int");

                    b.HasKey("PrioritiesId", "PrioritySchemesId");

                    b.HasIndex("PrioritySchemesId");

                    b.ToTable("PriorityPriorityScheme");
                });

            modelBuilder.Entity("WhatBug.Domain.Entities.Issue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AssigneeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<int>("LastModifiedBy")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.Property<int>("ReporterId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AssigneeId");

                    b.HasIndex("ProjectId");

                    b.HasIndex("ReporterId");

                    b.ToTable("Issues");
                });

            modelBuilder.Entity("WhatBug.Domain.Entities.Permissions.Permission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Permissions");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Create new projects.",
                            Name = "Create Project",
                            Type = "Global"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Delete existing projects.",
                            Name = "Delete Project",
                            Type = "Global"
                        },
                        new
                        {
                            Id = 3,
                            Description = "Edit global permissions assigned to users.",
                            Name = "Edit User Permissions",
                            Type = "Global"
                        },
                        new
                        {
                            Id = 4,
                            Description = "View all projects in WhatBug. Users without this permission must be a member of a project to view it.",
                            Name = "View All Projects",
                            Type = "Global"
                        });
                });

            modelBuilder.Entity("WhatBug.Domain.Entities.Permissions.ProjectRoleUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("ProjectRoleUsers");
                });

            modelBuilder.Entity("WhatBug.Domain.Entities.Permissions.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Administrator"
                        },
                        new
                        {
                            Id = 2,
                            Name = "User"
                        });
                });

            modelBuilder.Entity("WhatBug.Domain.Entities.Permissions.RolePermission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<int>("LastModifiedBy")
                        .HasColumnType("int");

                    b.Property<int>("PermissionId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<int>("SchemeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PermissionId");

                    b.HasIndex("RoleId");

                    b.HasIndex("SchemeId");

                    b.ToTable("RolePermissions");
                });

            modelBuilder.Entity("WhatBug.Domain.Entities.Permissions.Scheme", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Schemes");
                });

            modelBuilder.Entity("WhatBug.Domain.Entities.Permissions.UserPermission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<int>("LastModifiedBy")
                        .HasColumnType("int");

                    b.Property<int>("PermissionId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PermissionId");

                    b.HasIndex("UserId");

                    b.ToTable("UserPermissions");
                });

            modelBuilder.Entity("WhatBug.Domain.Entities.Priorities.Priority", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PriorityIconId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PriorityIconId");

                    b.ToTable("Priorities");
                });

            modelBuilder.Entity("WhatBug.Domain.Entities.Priorities.PriorityIcon", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PriorityIcons");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "ArrowUp"
                        },
                        new
                        {
                            Id = 2,
                            Name = "ArrowDown"
                        },
                        new
                        {
                            Id = 3,
                            Name = "ArrowLeft"
                        },
                        new
                        {
                            Id = 4,
                            Name = "ArrowRight"
                        },
                        new
                        {
                            Id = 5,
                            Name = "CircleArrowUp"
                        },
                        new
                        {
                            Id = 6,
                            Name = "CircleArrowDown"
                        },
                        new
                        {
                            Id = 7,
                            Name = "CircleArrowLeft"
                        },
                        new
                        {
                            Id = 8,
                            Name = "CircleArrowRight"
                        },
                        new
                        {
                            Id = 9,
                            Name = "ChevronUp"
                        },
                        new
                        {
                            Id = 10,
                            Name = "ChevronDown"
                        },
                        new
                        {
                            Id = 11,
                            Name = "ChevronLeft"
                        },
                        new
                        {
                            Id = 12,
                            Name = "ChevronRight"
                        },
                        new
                        {
                            Id = 13,
                            Name = "AngleUp"
                        },
                        new
                        {
                            Id = 14,
                            Name = "AngleDown"
                        },
                        new
                        {
                            Id = 15,
                            Name = "AngleLeft"
                        },
                        new
                        {
                            Id = 16,
                            Name = "AngleRight"
                        },
                        new
                        {
                            Id = 17,
                            Name = "AnglesUp"
                        },
                        new
                        {
                            Id = 18,
                            Name = "AnglesDown"
                        },
                        new
                        {
                            Id = 19,
                            Name = "AnglesLeft"
                        },
                        new
                        {
                            Id = 20,
                            Name = "AnglesRight"
                        },
                        new
                        {
                            Id = 21,
                            Name = "Exclaimation"
                        },
                        new
                        {
                            Id = 22,
                            Name = "CircleExclaimation"
                        },
                        new
                        {
                            Id = 23,
                            Name = "TriangleExclaimation"
                        },
                        new
                        {
                            Id = 24,
                            Name = "XMark"
                        },
                        new
                        {
                            Id = 25,
                            Name = "Ban"
                        });
                });

            modelBuilder.Entity("WhatBug.Domain.Entities.Priorities.PriorityScheme", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PrioritySchemes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "The default priority scheme used by all projects without any other scheme assigned.",
                            Name = "Default"
                        });
                });

            modelBuilder.Entity("WhatBug.Domain.Entities.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<int>("LastModifiedBy")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PrioritySchemeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PrioritySchemeId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("WhatBug.Domain.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("PriorityPriorityScheme", b =>
                {
                    b.HasOne("WhatBug.Domain.Entities.Priorities.Priority", null)
                        .WithMany()
                        .HasForeignKey("PrioritiesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WhatBug.Domain.Entities.Priorities.PriorityScheme", null)
                        .WithMany()
                        .HasForeignKey("PrioritySchemesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WhatBug.Domain.Entities.Issue", b =>
                {
                    b.HasOne("WhatBug.Domain.Entities.User", "Assignee")
                        .WithMany("AssignedIssues")
                        .HasForeignKey("AssigneeId");

                    b.HasOne("WhatBug.Domain.Entities.Project", "Project")
                        .WithMany("Issues")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WhatBug.Domain.Entities.User", "Reporter")
                        .WithMany("ReportedIssues")
                        .HasForeignKey("ReporterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Assignee");

                    b.Navigation("Project");

                    b.Navigation("Reporter");
                });

            modelBuilder.Entity("WhatBug.Domain.Entities.Permissions.ProjectRoleUser", b =>
                {
                    b.HasOne("WhatBug.Domain.Entities.Project", "Project")
                        .WithMany("RoleUsers")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WhatBug.Domain.Entities.Permissions.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WhatBug.Domain.Entities.User", "User")
                        .WithMany("ProjectRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("WhatBug.Domain.Entities.Permissions.RolePermission", b =>
                {
                    b.HasOne("WhatBug.Domain.Entities.Permissions.Permission", "Permission")
                        .WithMany()
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WhatBug.Domain.Entities.Permissions.Role", "Role")
                        .WithMany("RolePermissions")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WhatBug.Domain.Entities.Permissions.Scheme", "Scheme")
                        .WithMany()
                        .HasForeignKey("SchemeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Permission");

                    b.Navigation("Role");

                    b.Navigation("Scheme");
                });

            modelBuilder.Entity("WhatBug.Domain.Entities.Permissions.UserPermission", b =>
                {
                    b.HasOne("WhatBug.Domain.Entities.Permissions.Permission", "Permission")
                        .WithMany()
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WhatBug.Domain.Entities.User", "User")
                        .WithMany("UserPermissions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Permission");

                    b.Navigation("User");
                });

            modelBuilder.Entity("WhatBug.Domain.Entities.Priorities.Priority", b =>
                {
                    b.HasOne("WhatBug.Domain.Entities.Priorities.PriorityIcon", "PriorityIcon")
                        .WithMany("Priorities")
                        .HasForeignKey("PriorityIconId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PriorityIcon");
                });

            modelBuilder.Entity("WhatBug.Domain.Entities.Project", b =>
                {
                    b.HasOne("WhatBug.Domain.Entities.Priorities.PriorityScheme", "PriorityScheme")
                        .WithMany("Projects")
                        .HasForeignKey("PrioritySchemeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PriorityScheme");
                });

            modelBuilder.Entity("WhatBug.Domain.Entities.Permissions.Role", b =>
                {
                    b.Navigation("RolePermissions");
                });

            modelBuilder.Entity("WhatBug.Domain.Entities.Priorities.PriorityIcon", b =>
                {
                    b.Navigation("Priorities");
                });

            modelBuilder.Entity("WhatBug.Domain.Entities.Priorities.PriorityScheme", b =>
                {
                    b.Navigation("Projects");
                });

            modelBuilder.Entity("WhatBug.Domain.Entities.Project", b =>
                {
                    b.Navigation("Issues");

                    b.Navigation("RoleUsers");
                });

            modelBuilder.Entity("WhatBug.Domain.Entities.User", b =>
                {
                    b.Navigation("AssignedIssues");

                    b.Navigation("ProjectRoles");

                    b.Navigation("ReportedIssues");

                    b.Navigation("UserPermissions");
                });
#pragma warning restore 612, 618
        }
    }
}
