using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Domain.Common;
using WhatBug.Domain.Entities;
using WhatBug.Domain.Entities.JoinTables;
using WhatBug.Domain.Entities.Permissions;
using WhatBug.Domain.Entities.Priorities;

namespace WhatBug.Persistence
{
    public class WhatBugDbContext : DbContext, IWhatBugDbContext
    {
        private readonly ICurrentUserService _currentUserService;

        public WhatBugDbContext(DbContextOptions<WhatBugDbContext> options)
            : base(options)
        {
        }

        public WhatBugDbContext(DbContextOptions<WhatBugDbContext> options, ICurrentUserService currentUserService)
            : base(options)
        {
            _currentUserService = currentUserService;
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Issue> Issues { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ProjectRole> ProjectRoles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<ProjectRoleUser> ProjectRoleUsers { get; set; }
        public DbSet<PermissionScheme> PermissionSchemes { get; set; }
        public DbSet<Priority> Priorities { get; set; }
        public DbSet<PriorityScheme> PrioritySchemes { get; set; }
        public DbSet<Icon> Icons { get; set; }
        public DbSet<IssueType> IssueTypes { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<ColorIcon> ColorIcons { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = _currentUserService.UserId;
                        entry.Entity.Created = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = _currentUserService.UserId;
                        entry.Entity.LastModified = DateTime.Now;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Permission>()
                .Property(p => p.Type)
                .HasConversion(
                    r => r.ToString(),
                    r => (PermissionType)Enum.Parse(typeof(PermissionType), r));

            modelBuilder
                .Entity<UserPermission>()
                .HasKey(p => new { p.UserId, p.PermissionId });

            modelBuilder
                .Entity<UserPermission>()
                .HasOne(p => p.User)
                .WithMany(u => u.UserPermissions);

            modelBuilder
                .Entity<UserPermission>()
                .HasOne(p => p.Permission)
                .WithMany()
                .HasForeignKey(p => p.PermissionId);

            modelBuilder
                .Entity<PermissionSchemeProjectRolePermission>()
                .HasKey(p => new { p.PermissionSchemeId, p.ProjectRoleId, p.PermissionId });

            modelBuilder
                .Entity<PermissionSchemeProjectRolePermission>()
                .HasOne(p => p.PermissionScheme)
                .WithMany(s => s.ProjectRolePermissions)
                .HasForeignKey(p => p.PermissionSchemeId);

            modelBuilder
                .Entity<PermissionSchemeProjectRolePermission>()
                .HasOne(p => p.ProjectRole)
                .WithMany()
                .HasForeignKey(p => p.ProjectRoleId);

            modelBuilder
                .Entity<PermissionSchemeProjectRolePermission>()
                .HasOne(p => p.Permission)
                .WithMany()
                .HasForeignKey(p => p.PermissionId);

            modelBuilder
                .Entity<ProjectUserProjectRole>()
                .HasKey(p => new { p.ProjectId, p.UserId, p.ProjectRoleId });

            modelBuilder
                .Entity<ProjectUserProjectRole>()
                .HasOne(p => p.Project)
                .WithMany()
                .HasForeignKey(p => p.ProjectId);

            modelBuilder
                .Entity<ProjectUserProjectRole>()
                .HasOne(p => p.User)
                .WithMany()
                .HasForeignKey(p => p.UserId);

            modelBuilder
                .Entity<ProjectUserProjectRole>()
                .HasOne(p => p.ProjectRole)
                .WithMany()
                .HasForeignKey(p => p.ProjectRoleId);

            modelBuilder
                .Entity<Issue>()
                .HasOne(i => i.Assignee)
                .WithMany(u => u.AssignedIssues)
                .HasForeignKey(i => i.AssigneeId);

            modelBuilder
                .Entity<Issue>()
                .HasOne(i => i.Reporter)
                .WithMany(u => u.ReportedIssues)
                .HasForeignKey(i => i.ReporterId);

            modelBuilder
                .Entity<ColorIcon>()
                .HasOne(ci => ci.Color)
                .WithMany()
                .HasForeignKey(ci => ci.ColorId)
                .IsRequired();

            modelBuilder
                .Entity<ColorIcon>()
                .HasOne(ci => ci.Icon)
                .WithMany()
                .HasForeignKey(ci => ci.IconId)
                .IsRequired();

            modelBuilder
                .Entity<Issue>()
                .HasOne(i => i.IssueType)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Permission>().HasData(Domain.Data.Permissions.Seed());
            modelBuilder.Entity<Icon>().HasData(Domain.Data.Icons.Seed());
            modelBuilder.Entity<IssueType>().HasData(Domain.Data.IssueTypes.Seed());
            modelBuilder.Entity<Color>().HasData(Domain.Data.Colors.Seed());
            modelBuilder.Entity<ColorIcon>().HasData(Domain.Data.ColorIcons.Seed());
            modelBuilder.Entity<PriorityScheme>().HasData(new PriorityScheme() { Id = 1, Name = "Default", Description = "The default priority scheme used by all projects without any other scheme assigned." });
        }
    }
}
