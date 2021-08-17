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

namespace WhatBug.Application.Priorities.Queries.GetCreatePriority
{
    public class GetCreatePriorityQueryHandler : IRequestHandler<GetCreatePriorityQuery, CreatePriorityDTO>
    {
        private readonly IWhatBugDbContext _context;
        private readonly IMapper _mapper;

        public GetCreatePriorityQueryHandler(IWhatBugDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CreatePriorityDTO> Handle(GetCreatePriorityQuery request, CancellationToken cancellationToken)
        {
            var dto = new CreatePriorityDTO
            {
                Colors = await _mapper.ProjectTo<ColorDTO>(_context.Colors).ToListAsync(),
                Icons = await _mapper.ProjectTo<IconDTO>(_context.Icons).ToListAsync()
            };
            return dto;
        }
    }
}
