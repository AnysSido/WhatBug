using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;

namespace WhatBug.Application.PrioritySchemes.Queries.GetEditPriorityScheme
{
    public class GetEditPrioritySchemeQueryHandler : IRequestHandler<GetEditPrioritySchemeQuery, EditPrioritySchemeDTO>
    {
        private readonly IWhatBugDbContext _context;
        private readonly IMapper _mapper;

        public GetEditPrioritySchemeQueryHandler(IWhatBugDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<EditPrioritySchemeDTO> Handle(GetEditPrioritySchemeQuery request, CancellationToken cancellationToken)
        {
            var dto = await _mapper.ProjectTo<EditPrioritySchemeDTO>(_context.PrioritySchemes).FirstOrDefaultAsync(s => s.Id == request.Id);

            if (dto == null)
            {
                // TODO: Throw not found exception
            }

            dto.Priorities = await _mapper.ProjectTo<PriorityDTO>(_context.Priorities).ToListAsync();
            return dto;
        }
    }
}
