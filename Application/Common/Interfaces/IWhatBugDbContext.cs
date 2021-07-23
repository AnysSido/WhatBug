using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Domain.Entities;
using WhatBug.Domain.Entities.Permissions;
using WhatBug.Domain.Entities.Priorities;

namespace WhatBug.Application.Common.Interfaces
{
    public interface IWhatBugDbContext
    {
        DbSet<Project> Projects { get; set; }
        DbSet<Issue> Issues { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<Permission> Permissions { get; set; }
        DbSet<RolePermission> RolePermissions { get; set; }
        DbSet<UserPermission> UserPermissions { get; set; }
        DbSet<ProjectRoleUser> ProjectRoleUsers { get; set; }
        DbSet<Role> Roles { get; set; }
        DbSet<PermissionScheme> PermissionSchemes { get; set; }
        DbSet<Priority> Priorities { get; set; }
        DbSet<PriorityScheme> PrioritySchemes { get; set; }
        DbSet<Icon> Icons { get; set; }
        DbSet<Color> Colors { get; set; }
        DbSet<IssueType> IssueTypes { get; set; }
        DbSet<ProjectRole> ProjectRoles { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
