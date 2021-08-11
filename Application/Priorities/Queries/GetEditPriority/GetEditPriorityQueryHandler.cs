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

namespace WhatBug.Application.Priorities.Queries.GetEditPriority
{
    public class GetEditPriorityQueryHandler : IRequestHandler<GetEditPriorityQuery, EditPriorityDTO>
    {
        private readonly IWhatBugDbContext _context;
        private readonly IMapper _mapper;

        public GetEditPriorityQueryHandler(IWhatBugDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<EditPriorityDTO> Handle(GetEditPriorityQuery request, CancellationToken cancellationToken)
        {
            var dto = await _mapper.ProjectTo<EditPriorityDTO>(_context.Priorities).SingleOrDefaultAsync(p => p.Id == request.Id);

            if (dto == null)
            {
                // TODO: Throw not found exception
            }

            dto.Colors = await _mapper.ProjectTo<ColorDTO>(_context.Colors).ToListAsync();
            dto.Icons = await _mapper.ProjectTo<IconDTO>(_context.Icons).ToListAsync();

            return dto;
        }
    }
}
