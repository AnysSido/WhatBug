using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;

namespace WhatBug.Application.Priorities.Queries.GetPriorities
{
    public class GetPrioritiesQueryHandler : IRequestHandler<GetPrioritiesQuery, PrioritiesDTO>
    {
        private readonly IWhatBugDbContext _context;
        private readonly IMapper _mapper;

        public GetPrioritiesQueryHandler(IWhatBugDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PrioritiesDTO> Handle(GetPrioritiesQuery request, CancellationToken cancellationToken)
        {
            var priorities = await _mapper.ProjectTo<PriorityDTO>(_context.Priorities).OrderBy(p => p.Order).ToListAsync();
            return new PrioritiesDTO { Priorities = priorities };
        }
    }
}
