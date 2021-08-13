using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Application.Services.Interfaces;
using Data = WhatBug.Domain.Data;

namespace WhatBug.Application.Services
{
    class PermissionService : IPermissionService
    {
        private readonly IWhatBugDbContext _context;

        public PermissionService(IWhatBugDbContext context)
        {
            _context = context;
        }

        public async Task<bool> UserHasPermission(int userId, string permission)
        {
            var permissionEntity = Data.Permissions.ToEntity(permission);

            var hasPermission = await _context.Users
                .Where(u => u.Id == userId && u.UserPermissions.Select(p => p.Permission).Contains(permissionEntity))
                .AnyAsync();

            return hasPermission;
        }
    }
}
