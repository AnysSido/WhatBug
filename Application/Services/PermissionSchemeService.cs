using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Application.DTOs.Permissions;
using WhatBug.Application.DTOs.PermissionSchemes;
using WhatBug.Application.Services.Interfaces;
using Data = WhatBug.Domain.Data;
using WhatBug.Domain.Entities;
using WhatBug.Domain.Entities.JoinTables;

namespace WhatBug.Application.Services
{
    class PermissionSchemeService : IPermissionSchemeService
    {
        private readonly IWhatBugDbContext _context;
        private readonly IMapper _mapper;

        public PermissionSchemeService(IWhatBugDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<PermissionDTO> GetAvailableProjectRolePermissions()
        {
            return _mapper.Map<List<PermissionDTO>>(Data.Permissions.GetAll(PermissionType.Project).ToList());
        }

        public async Task<PermissionSchemeDTO> GetPermissionSchemeAsync(int id)
        {
            // TODO: Check permissions
            return _mapper.Map<PermissionSchemeDTO>(await _context.PermissionSchemes.FirstAsync(s => s.Id == id));
        }

        public async Task<List<PermissionDTO>> GetProjectRolePermissionsAsync(int schemeId, int projectRoleId)
        {
            // TODO: Check permissions
            var scheme = await _context.PermissionSchemes
                .Include(s => s.ProjectRolePermissions.Where(o => o.RoleId == projectRoleId))
                    .ThenInclude(s => s.Permission)
                .FirstOrDefaultAsync(s => s.Id == schemeId);

            return _mapper.Map<List<PermissionDTO>>(scheme.ProjectRolePermissions.Select(p => p.Permission).ToList());
        }

        public async Task SetProjectRolePermissionsAsync(SetProjectRolePermissionsDTO dto)
        {
            // TODO: Check permissions
            var scheme = await _context.PermissionSchemes.Include(s => s.ProjectRolePermissions).FirstAsync(s => s.Id == dto.SchemeId);
            var projectRole = await _context.Roles.FirstAsync(r => r.Id == dto.ProjectRoleId);
            var permissions = await _context.Permissions.Where(p => dto.GrantedPermissionIds.Contains(p.Id)).ToListAsync();

            var grantedRolePermissions = permissions.Select(p => new PermissionSchemeRolePermission
            {
                PermissionScheme = scheme,
                Role = projectRole,
                Permission = p
            });

            scheme.ProjectRolePermissions.RemoveAll(p => p.RoleId == dto.ProjectRoleId);
            scheme.ProjectRolePermissions.AddRange(grantedRolePermissions);

            await _context.SaveChangesAsync();
        }
    }
}
