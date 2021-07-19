using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Application.DTOs.Common;
using WhatBug.Application.DTOs.Priorities;
using WhatBug.Application.Services.Interfaces;
using WhatBug.Domain.Entities;
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

        public async Task CreatePriorityAsync(CreatePriorityDTO dto)
        {
            // TODO: Check permissions, validate color
            var priority = _mapper.Map<Priority>(dto);
            priority.Order = await _context.Priorities.MaxAsync(p => (int?)p.Order) + 1 ?? 1;
            await _context.Priorities.AddAsync(priority);
            await _context.SaveChangesAsync();
        }

        public async Task EditPriorityAsync(EditPriorityDTO dto)
        {
            // TODO: Check permissions, validate color
            var priority = await _context.Priorities.Include(p => p.ColorIcon).FirstAsync(p => p.Id == dto.Id);
            _mapper.Map(dto, priority);
            await _context.SaveChangesAsync();
        }

        public async Task<PriorityDTO> GetPriorityAsync(int id)
        {
            return _mapper.Map<PriorityDTO>(await _context.Priorities
                .Include(p => p.ColorIcon.Color)
                .Include(p => p.ColorIcon.Icon)
                .FirstOrDefaultAsync(p => p.Id == id));
        }

        public async Task<List<PriorityDTO>> GetPrioritiesAsync()
        {
            // Requires permission?
            return _mapper.Map<List<PriorityDTO>>(await _context.Priorities
                .Include(p => p.ColorIcon.Color)
                .Include(p => p.ColorIcon.Icon)
                .ToListAsync())
                .OrderBy(p => p.Order).ToList();
        }

        public async Task<List<IconDTO>> LoadIconsAsync()
        {
            return _mapper.Map<List<IconDTO>>(await _context.Icons.ToListAsync());
        }

        public async Task UpdatePriorityOrderAsync(List<int> ids)
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
