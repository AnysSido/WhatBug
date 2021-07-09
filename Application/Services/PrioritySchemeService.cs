using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Application.DTOs.Priorities;
using WhatBug.Application.Services.Interfaces;
using WhatBug.Domain.Entities.Priorities;

namespace WhatBug.Application.Services
{
    class PrioritySchemeService : IPrioritySchemeService
    {
        private readonly IWhatBugDbContext _context;
        private readonly IMapper _mapper;

        public PrioritySchemeService(IWhatBugDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<PrioritySchemeDTO>> GetPrioritySchemesAsync()
        {
            // TODO: Check permission
            var prioritySchemes = await _context.PrioritySchemes.ToListAsync();
            return _mapper.Map<List<PrioritySchemeDTO>>(prioritySchemes);
        }

        public async Task CreatePrioritySchemeAsync(CreatePrioritySchemeDTO dto)
        {
            // TODO: Check permission
            var scheme = _mapper.Map<PriorityScheme>(dto);
            var priorities = await _context.Priorities.Where(p => dto.PriorityIds.Contains(p.Id)).ToListAsync();
            scheme.Priorities = priorities;

            await _context.PrioritySchemes.AddAsync(scheme);
            await _context.SaveChangesAsync();
        }
    }
}
