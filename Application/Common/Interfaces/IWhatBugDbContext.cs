﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Domain.Entities;
using WhatBug.Domain.Entities.Permissions;

namespace WhatBug.Application.Common.Interfaces
{
    public interface IWhatBugDbContext
    {
        DbSet<Project> Projects { get; set; }
        DbSet<Issue> Issues { get; set; }
        DbSet<User> Users { get; set; }    
        DbSet<Permission> Permissions { get; set; }
        DbSet<ProjectRoleUser> ProjectRoleUsers { get; set; }
        DbSet<Role> Roles { get; set; }
        DbSet<SchemeRolePermission> SchemeRolePermissions { get; set; }
        DbSet<Scheme> Schemes { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
