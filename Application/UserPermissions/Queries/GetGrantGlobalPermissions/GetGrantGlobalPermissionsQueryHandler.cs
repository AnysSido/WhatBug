﻿using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Domain.Entities;

namespace WhatBug.Application.UserPermissions.Queries.GetGrantGlobalPermissions
{
    public class GetGrantGlobalPermissionsQueryHandler : IRequestHandler<GetGrantGlobalPermissionsQuery, GrantGlobalPermissionsDTO>
    {
        public IWhatBugDbContext _context;
        public IMapper _mapper;

        public GetGrantGlobalPermissionsQueryHandler(IWhatBugDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GrantGlobalPermissionsDTO> Handle(GetGrantGlobalPermissionsQuery request, CancellationToken cancellationToken)
        {
            var grantedPermissions = await _context.Users.Include(u => u.UserPermissions).ThenInclude(u => u.Permission)
                .FirstOrDefaultAsync(u => u.Id == request.UserId);

            var grantedPermissionIds = grantedPermissions.UserPermissions.Select(p => p.Permission.Id);

            var globalPermissions = await _mapper.ProjectTo<PermissionDTO>(_context.Permissions.Where(p => p.Type == PermissionType.Global)).ToListAsync();

            foreach (var permission in globalPermissions)
            {
                if (grantedPermissionIds.Contains(permission.Id))
                {
                    permission.IsGranted = true;
                }
            }

            var dto = new GrantGlobalPermissionsDTO
            {
                UserId = request.UserId,
                Permissions = globalPermissions,
                PermissionIds = grantedPermissionIds
            };

            return dto;
        }
    }
}