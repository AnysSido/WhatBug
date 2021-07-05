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
        public DbSet<Project> Projects { get; set; }
        public DbSet<Issue> Issues { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<UserPermission> UserPermissions { get; set; }
        public DbSet<ProjectRoleUser> ProjectRoleUsers { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Scheme> Schemes { get; set; }
        DbSet<Priority> Priorities { get; set; }
        DbSet<PriorityScheme> PrioritySchemes { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
