using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Application.DTOs.PrioritySchemes;
using WhatBug.Application.Services.Interfaces;
using WhatBug.Domain.Entities;

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

        public async Task<PrioritySchemeDTO> GetPrioritySchemeAsync(int id)
        {
            // TODO: Check permission
            var priorityScheme = await _context.PrioritySchemes
                .Include(s => s.Priorities)
                    .ThenInclude(p => p.ColorIcon.Color)
                 .Include(s => s.Priorities)
                    .ThenInclude(p => p.ColorIcon.Icon)
                .FirstOrDefaultAsync(s => s.Id == id);
            priorityScheme.Priorities = priorityScheme.Priorities.OrderBy(p => p.Id).ToList();
            return _mapper.Map<PrioritySchemeDTO>(priorityScheme);
        }

        public async Task CreatePrioritySchemeAsync(CreatePrioritySchemeDTO dto)
        {
            // TODO: Check permission
            var scheme = _mapper.Map<PriorityScheme>(dto);
            scheme.Priorities = await _context.Priorities.Where(p => dto.PriorityIds.Contains(p.Id)).ToListAsync();

            await _context.PrioritySchemes.AddAsync(scheme);
            await _context.SaveChangesAsync();
        }

        public async Task EditPrioritySchemeAsync(EditPrioritySchemeDTO dto)
        {
            // TODO: Check permission
            var scheme = await _context.PrioritySchemes
                .Include(s => s.Priorities)
                .FirstAsync(s => s.Id == dto.Id);

            _mapper.Map(dto, scheme);
            scheme.Priorities = await _context.Priorities.Where(p => dto.PriorityIds.Contains(p.Id)).ToListAsync();
            await _context.SaveChangesAsync();
        }
    }
}
