using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Domain.Entities;

namespace WhatBug.Application.Common.Interfaces
{
    public interface IWhatBugDbContext
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

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
