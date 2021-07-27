using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Application.DTOs.Admin;
using WhatBug.Application.Services.Interfaces;
using WhatBug.Domain.Entities;

namespace WhatBug.Application.Services
{
    class AdminService : IAdminService
    {
        private readonly IWhatBugDbContext _context;
        private readonly IMapper _mapper;

        public AdminService(IWhatBugDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ProjectRoleDTO>> GetProjectRolesAsync()
        {
            return _mapper.Map<List<ProjectRoleDTO>>(await _context.ProjectRoles.ToListAsync());
        }

        public async Task CreateProjectRole(CreateProjectRoleDTO dto)
        {
            // TODO: Check permission
            await _context.ProjectRoles.AddAsync(_mapper.Map<ProjectRole>(dto));
            await _context.SaveChangesAsync();
        }
    }
}
