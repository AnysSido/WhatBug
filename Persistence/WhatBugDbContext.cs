using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Domain.Common;
using WhatBug.Domain.Entities;

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
        public DbSet<Role> Roles { get; set; }
        public DbSet<ProjectRoleUser> ProjectRoleUsers { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<PermissionScheme> PermissionSchemes { get; set; }
        public DbSet<Priority> Priorities { get; set; }
        public DbSet<PrioritySchemePriority> PrioritySchemePriorities { get; set; }
        public DbSet<PriorityScheme> PrioritySchemes { get; set; }
        public DbSet<Icon> Icons { get; set; }
        public DbSet<IssueType> IssueTypes { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<IssueStatus> IssueStatuses { get; set; }
        public DbSet<IssueComment> IssueComments { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<UserPermission> UserPermissions { get; set; }
        public DbSet<PermissionSchemeRolePermission> PermissionSchemeRolePermissions { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = _currentUserService.Id;
                        entry.Entity.Created = DateTime.Now;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = _currentUserService.Id;
                        entry.Entity.LastModified = DateTime.Now;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>()
                .Property(p => p.Key)
                .IsRequired();

            modelBuilder
                .Entity<ProjectRoleUser>()
                .HasKey(p => new { p.ProjectId, p.RoleId, p.UserId });

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
                .Entity<PermissionSchemeRolePermission>()
                .HasKey(p => new { p.PermissionSchemeId, p.RoleId, p.PermissionId });

            modelBuilder
                .Entity<PermissionSchemeRolePermission>()
                .HasOne(p => p.PermissionScheme)
                .WithMany(s => s.RolePermissions)
                .HasForeignKey(p => p.PermissionSchemeId);

            //modelBuilder
            //    .Entity<PermissionSchemeRolePermission>()
            //    .HasOne(p => p.Role)
            //    .WithMany()
            //    .HasForeignKey(p => p.RoleId);

            //modelBuilder
            //    .Entity<PermissionSchemeRolePermission>()
            //    .HasOne(p => p.Permission)
            //    .WithMany()
            //    .HasForeignKey(p => p.PermissionId);

            modelBuilder
                .Entity<PrioritySchemePriority>()
                .HasKey(s => new { s.PrioritySchemeId, s.PriorityId });

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
                .Entity<Issue>()
                .HasOne(i => i.IssueType)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Permission>().HasData(Domain.Data.Permissions.Seed());
            modelBuilder.Entity<Icon>().HasData(Domain.Data.Icons.Seed());
            modelBuilder.Entity<IssueType>().HasData(Domain.Data.IssueTypes.Seed());
            modelBuilder.Entity<Color>().HasData(Domain.Data.Colors.Seed());

            modelBuilder.Entity<Priority>()
                .HasData(new Priority
                {
                    Id = 1,
                    Name = "Default",
                    Description = "The default priority used by all issues without any other priority assigned.",
                    IsDefault = true,
                    Order = 0,
                    IconId = Domain.Data.Icons.WaveSquare.Id,
                    ColorId = Domain.Data.Colors.Black.Id
                });

            modelBuilder.Entity<PriorityScheme>()
                .HasData(new PriorityScheme
                {
                    Id = 1,
                    Name = "Default",
                    Description = "The default priority scheme used by all projects without any other scheme assigned.",
                    IsDefault = true
                });

            modelBuilder.Entity<PermissionScheme>()
                .HasData(new PermissionScheme
                {
                    Id = 1,
                    IsDefault = true,
                    Name = "Default",
                    Description = "The default permission scheme used by all projects without any other scheme assigned."
                });

            // TODO: Clean this up
            modelBuilder.Entity<IssueStatus>().HasData(new IssueStatus { Id = 1, Name = "Backlog" }, new IssueStatus { Id = 2, Name = "ToDo" }, new IssueStatus { Id = 3, Name = "In Progress" }, new IssueStatus { Id = 4, Name = "Done" });
        }
    }
}