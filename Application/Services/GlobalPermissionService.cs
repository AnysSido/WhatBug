using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Application.DTOs.GlobalPermissions;
using WhatBug.Application.DTOs.Permissions;
using WhatBug.Application.Services.Interfaces;
using WhatBug.Domain.Data;
using WhatBug.Domain.Entities;
using WhatBug.Domain.Entities.JoinTables;

namespace WhatBug.Application.Services
{
    class GlobalPermissionService : IGlobalPermissionService
    {
        private readonly IWhatBugDbContext _context;
        private readonly IMapper _mapper;

        public GlobalPermissionService(IWhatBugDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<PermissionDTO> GetAvailableGlobalPermissions()
        {
            // TODO: Check permissions
            return _mapper.Map<List<PermissionDTO>>(Permissions.GetAll(PermissionType.Global).ToList());
        }

        public async Task<List<PermissionDTO>> GetUserGlobalPermissionsAsync(int userId)
        {
            // TODO: Check permissions
            var user = await _context.Users
                .Include(u => u.UserPermissions)
                    .ThenInclude(u => u.Permission)
                .FirstOrDefaultAsync(u => u.Id == userId);

            return _mapper.Map<List<PermissionDTO>>(user.UserPermissions.Select(u => u.Permission));
        }

        public async Task SetGlobalPermissionsAsync(SetGlobalPermissionsDTO dto)
        {
            // TODO: Check permissions
            var user = await _context.Users
                .Include(u => u.UserPermissions)
                    .ThenInclude(p => p.Permission)
                .Where(u => u.Id == dto.UserId).FirstAsync();

            var permissionsToGrant = await _context.Permissions.Where(p => dto.GrantedPermissionIds.Contains(p.Id)).ToListAsync();

            user.UserPermissions.Clear();
            user.UserPermissions.AddRange(permissionsToGrant.Select(p => new UserPermission { User = user, Permission = p }));
            await _context.SaveChangesAsync();
        }
    }
}
