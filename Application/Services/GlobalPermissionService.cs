using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Application.DTOs.Permissions;
using WhatBug.Application.Services.Interfaces;

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

        public async Task<List<PermissionDTO>> GetUserGlobalPermissionsAsync(int userId)
        {
            // TODO: Check permissions
            var user = await _context.Users
                .Include(u => u.UserPermissions)
                    .ThenInclude(u => u.Permission)
                .FirstOrDefaultAsync(u => u.Id == userId);

            return _mapper.Map<List<PermissionDTO>>(user.UserPermissions.Select(u => u.Permission));
        }
    }
}
