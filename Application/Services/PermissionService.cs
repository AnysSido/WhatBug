using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Application.DTOs.Permissions;
using WhatBug.Application.Services.Interfaces;
using WhatBug.Domain.Entities.Permissions;
using WhatBug.Domain.Data;
using WhatBug.Application.Common.Models;
using AutoMapper;
using WhatBug.Domain.Entities;

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
            var permissionEntity = Permissions.ToEntity(permission);

            var hasPermission = await _context.Users
                .Where(u => u.Id == userId && u.UserPermissions.Select(p => p.Permission).Contains(permissionEntity))
                .AnyAsync();

            return hasPermission;
        }
    }
}
