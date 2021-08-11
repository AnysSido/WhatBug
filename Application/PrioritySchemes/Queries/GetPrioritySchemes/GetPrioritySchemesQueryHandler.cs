using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;

namespace WhatBug.Application.PrioritySchemes.Queries.GetPrioritySchemes
{
    public class GetPrioritySchemesQueryHandler : IRequestHandler<GetPrioritySchemesQuery, PrioritySchemesDTO>
    {
        private readonly IWhatBugDbContext _context;
        private readonly IMapper _mapper;

        public GetPrioritySchemesQueryHandler(IWhatBugDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PrioritySchemesDTO> Handle(GetPrioritySchemesQuery request, CancellationToken cancellationToken)
        {
            var schemes = await _mapper.ProjectTo<PrioritySchemeDTO>(_context.PrioritySchemes).ToListAsync();
            var dto = new PrioritySchemesDTO
            {
                PrioritySchemes = schemes
            };
            return dto;
        }
    }
}
