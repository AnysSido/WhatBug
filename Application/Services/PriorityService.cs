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
    public class PriorityService : IPriorityService
    {
        private readonly IWhatBugDbContext _context;
        private readonly IMapper _mapper;

        public PriorityService(IWhatBugDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task CreatePriorityAsync(PriorityDTO dto)
        {
            // TODO: Check permissions, validate color
            var priority = _mapper.Map<Priority>(dto);
            priority.PriorityIcon = await _context.PriorityIcons.FirstAsync(i => i.Name == dto.PriorityIcon.Name);
            await _context.Priorities.AddAsync(priority);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePriority(PriorityDTO dto)
        {
            // TODO: Check permissions, validate color
            var priority = await _context.Priorities.FirstAsync(p => p.Id == dto.Id);
            priority.Name = dto.Name;
            priority.Description = dto.Description;
            priority.Color = dto.Color;
            priority.PriorityIcon = await _context.PriorityIcons.FirstAsync(i => i.Name == dto.PriorityIcon.Name);
            await _context.SaveChangesAsync();
        }

        public async Task<PriorityDTO> GetPriorityAsync(int id)
        {
            return _mapper.Map<PriorityDTO>(await _context.Priorities
                .Include(p => p.PriorityIcon)
                .FirstOrDefaultAsync(p => p.Id == id));
        }

        public async Task<List<PriorityDTO>> GetPrioritiesAsync()
        {
            // Requires permission?
            return _mapper.Map<List<PriorityDTO>>(await _context.Priorities
                .Include(p => p.PriorityIcon)
                .ToListAsync());
        }

        public async Task<List<PriorityIconDTO>> LoadIconsAsync()
        {
            return _mapper.Map<List<PriorityIconDTO>>(await _context.PriorityIcons.ToListAsync());
        }

        public async Task UpdatePriorityOrder(List<int> ids)
        {
            // TODO: Check permissions
            var priorities = await _context.Priorities.ToListAsync();
            foreach (var priority in priorities)
            {
                priority.Order = ids.IndexOf(priority.Id);
            }
            await _context.SaveChangesAsync();
        }
    }
}
