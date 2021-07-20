using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Application.DTOs.Permissions;
using WhatBug.Application.Services.Interfaces;
using WhatBug.Domain.Entities.Permissions;

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

        public async Task CreatePermissionScheme(CreatePermissionSchemeDTO dto)
        {
            // TODO: Check permissions
            var permissionScheme = _mapper.Map<PermissionScheme>(dto);
            await _context.PermissionSchemes.AddAsync(permissionScheme);
            await _context.SaveChangesAsync();
        }
    }
}
