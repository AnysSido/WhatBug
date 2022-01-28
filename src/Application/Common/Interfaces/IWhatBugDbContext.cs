using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Domain.Entities;

namespace WhatBug.Application.Common.Interfaces
{
    public interface IWhatBugDbContext : IDisposable
    {
        DbSet<Project> Projects { get; set; }
        DbSet<Issue> Issues { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<Permission> Permissions { get; set; }
        DbSet<PermissionScheme> PermissionSchemes { get; set; }
        DbSet<Priority> Priorities { get; set; }
        DbSet<PriorityScheme> PrioritySchemes { get; set; }
        DbSet<Icon> Icons { get; set; }
        DbSet<Color> Colors { get; set; }
        DbSet<IssueType> IssueTypes { get; set; }
        DbSet<Role> Roles { get; set; }
        DbSet<IssueStatus> IssueStatuses { get; set; }
        DbSet<IssueComment> IssueComments { get; set; }
        DbSet<Attachment> Attachments { get; set; }
        DbSet<UserPermission> UserPermissions { get; set; }
        DbSet<PermissionSchemeRolePermission> PermissionSchemeRolePermissions { get; set; }
        DbSet<PrioritySchemePriority> PrioritySchemePriorities { get; set; }
        DbSet<ProjectRoleUser> ProjectRoleUsers { get; set; }

        ChangeTracker ChangeTracker { get; }
        DatabaseFacade Database { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        int SaveChanges();
    }
}
