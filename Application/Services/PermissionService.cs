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
using WhatBug.Domain.Exceptions;
using WhatBug.Application.DTOs.Users;
using AutoMapper;
using WhatBug.Domain.Entities.JoinTables;

namespace WhatBug.Application.Services
{
    class PermissionService : IPermissionService
    {
        private readonly IWhatBugDbContext _context;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public PermissionService(IWhatBugDbContext context, ICurrentUserService currentUserService, IMapper mapper)
        {
            _context = context;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }

        public async Task SetUserPermissions(SetUserPermissionsDTO setUserPermissionDTO)
        {
            if (!await UserHasPermission(_currentUserService.UserId, Permissions.EditUserPermissions))
                throw new InsufficientPermissionException(_currentUserService.UserId, Permissions.EditUserPermissions);

            var user = await _context.Users
                .Include(u => u.UserPermissions)
                    .ThenInclude(p => p.Permission)
                .FirstOrDefaultAsync(u => u.Id == setUserPermissionDTO.UserId);

            var permissionsToGrant = await _context.Permissions.Where(p => setUserPermissionDTO.PermissionIds.Contains(p.Id)).ToListAsync();

            user.UserPermissions.Clear();
            user.UserPermissions.AddRange(permissionsToGrant.Select(p => new UserPermission { User = user, Permission = p }));

            await _context.SaveChangesAsync();
            return;
        }

        public async Task SetUserProjectRole(SetUserProjectRoleDTO setUserProjectRoleDTO)
        {
            var newRoleUser = new ProjectRoleUser
            {
                ProjectId = setUserProjectRoleDTO.ProjectId,
                UserId = setUserProjectRoleDTO.UserId,
                RoleId = setUserProjectRoleDTO.RoleId
            };

            await _context.ProjectRoleUsers.AddAsync(newRoleUser);
            await _context.SaveChangesAsync();
        }

        public async Task SetRolePermissions(SetRolePermissionsDTO setRolePermissionsDTO)
        {

        }

        public async Task<bool> UserHasPermission(int userId, string permission)
        {
            var permissionEntity = Permissions.ToEntity(permission);

            var hasPermission = await _context.Users
                .Where(u => u.Id == userId && u.UserPermissions.Select(p => p.Permission).Contains(permissionEntity))
                .AnyAsync();

            return hasPermission;
        }

        public async Task<List<PermissionDTO>> GetUserPermissions(int userId)
        {
            var user = await _context.Users
                .Include(u => u.UserPermissions)
                    .ThenInclude(p => p.Permission)
                .FirstOrDefaultAsync(u => u.Id == userId);

            return _mapper.Map<List<PermissionDTO>>(user.UserPermissions.Select(p => p.Permission));
        }

        public List<PermissionDTO> GetAllPermissions(PermissionType permissionType)
        {
            return _mapper.Map<List<PermissionDTO>>(Permissions.GetAll(permissionType).ToList());
        }
    }
}
