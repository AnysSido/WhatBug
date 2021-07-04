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

        public async Task SetUserPermissions(SetUserPermissionDTO setUserPermissionDTO)
        {
            if (!await UserHasPermission(_currentUserService.UserId, Permissions.EditUserPermissions))
                throw new InsufficientPermissionException(_currentUserService.UserId, Permissions.EditUserPermissions);

            var user = await _context.Users
                .Where(u => u.Id == setUserPermissionDTO.UserId)
                .Include(u => u.UserPermissions).FirstOrDefaultAsync();

            if (user == null)
                throw new UserNotFoundException(setUserPermissionDTO.UserId);

            var newPermissions = setUserPermissionDTO.Permissions
                .Select(p => new UserPermission() { UserId = user.Id, PermissionId = p.Id });

            user.UserPermissions.Clear();
            user.UserPermissions.AddRange(newPermissions);

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

        public async Task<bool> UserHasPermission(int userId, Permission permission)
        {
            var hasPermission = await _context.UserPermissions
                .Where(p => p.UserId == userId)
                .Where(p => p.PermissionId == permission.Id)
                .FirstOrDefaultAsync();

            return hasPermission != null;
        }

        public List<PermissionDTO> GetAllPermissions(PermissionType permissionType)
        {
            return _mapper.Map<List<PermissionDTO>>(Permissions.GetAll(PermissionType.Global).ToList());
        }
    }
}
