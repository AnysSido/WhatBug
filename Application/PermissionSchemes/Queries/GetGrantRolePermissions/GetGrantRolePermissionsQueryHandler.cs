using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Domain.Entities;

namespace WhatBug.Application.PermissionSchemes.Queries.GetGrantRolePermissions
{
    public class GetGrantRolePermissionsQueryHandler : IRequestHandler<GetGrantRolePermissionsQuery, GrantRolePermissionsDTO>
    {
        private readonly IWhatBugDbContext _context;
        private readonly IMapper _mapper;

        public GetGrantRolePermissionsQueryHandler(IWhatBugDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GrantRolePermissionsDTO> Handle(GetGrantRolePermissionsQuery request, CancellationToken cancellationToken)
        {
            var scheme = await _mapper.ProjectTo<GrantRolePermissionsDTO>(_context.PermissionSchemes).FirstOrDefaultAsync(s => s.Id == request.SchemeId);
            var permissions = await _mapper.ProjectTo<PermissionDTO>(_context.Permissions.Where(p => p.Type == PermissionType.Project)).ToListAsync();
            var role = await _context.Roles.FirstOrDefaultAsync(r => r.Id == request.RoleId);

            permissions.ForEach(p => p.IsGranted = scheme.Permissions.Select(p => p.Id).Contains(p.Id));
            scheme.Permissions = permissions;
            scheme.RoleId = role.Id;
            scheme.RoleName = role.Name;

            return scheme;
        }
    }
}
