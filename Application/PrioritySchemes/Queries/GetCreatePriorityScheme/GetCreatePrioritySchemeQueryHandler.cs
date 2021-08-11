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

namespace WhatBug.Application.PrioritySchemes.Queries.GetCreatePriorityScheme
{
    public class GetCreatePrioritySchemeQueryHandler : IRequestHandler<GetCreatePrioritySchemeQuery, CreatePrioritySchemeDTO>
    {
        private readonly IWhatBugDbContext _context;
        private readonly IMapper _mapper;

        public GetCreatePrioritySchemeQueryHandler(IWhatBugDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CreatePrioritySchemeDTO> Handle(GetCreatePrioritySchemeQuery request, CancellationToken cancellationToken)
        {
            var dto = new CreatePrioritySchemeDTO
            {
                Priorities = await _mapper.ProjectTo<PriorityDTO>(_context.Priorities).ToListAsync()
            };
            return dto;
        }
    }
}
