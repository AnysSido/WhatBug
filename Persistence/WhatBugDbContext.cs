﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Domain.Common;
using WhatBug.Domain.Entities;
using WhatBug.Domain.Entities.Permissions;

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
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<UserPermission> UserPermissions { get; set; }
        public DbSet<ProjectRoleUser> ProjectRoleUsers { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Scheme> Schemes { get; set; }

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

            //modelBuilder
            //    .Entity<RolePermission>()
            //    .HasOne(p => p.Permission)
            //    .WithMany();

            //modelBuilder
            //    .Entity<UserPermission>()
            //    .HasOne(p => p.Permission)
            //    .WithMany();
                

            modelBuilder.Entity<Permission>().HasData(Domain.Data.Permissions.GetAll());
            modelBuilder.Entity<Role>().HasData(Domain.Data.Roles.GetAll());
        }
    }
}