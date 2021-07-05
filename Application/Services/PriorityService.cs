using AutoMapper;
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
            // TODO: Check permissions
            var priority = _mapper.Map<Priority>(dto);
            await _context.Priorities.AddAsync(priority);
        }
    }
}
