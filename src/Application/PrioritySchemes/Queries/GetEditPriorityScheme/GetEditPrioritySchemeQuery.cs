using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Application.Common.MediatR;
using WhatBug.Application.Common.Security;
using WhatBug.Domain.Data;

namespace WhatBug.Application.PrioritySchemes.Queries.GetEditPriorityScheme
{
    [Authorize(Permissions.ManagePrioritySchemes)]
    public record GetEditPrioritySchemeQuery : IQuery<Response<GetEditPrioritySchemeQueryResult>>
    {
        public int Id { get; init; }
    }

    public class GetEditPrioritySchemeQueryHandler : IRequestHandler<GetEditPrioritySchemeQuery, Response<GetEditPrioritySchemeQueryResult>>
    {
        private readonly IWhatBugDbContext _context;
        private readonly IMapper _mapper;

        public GetEditPrioritySchemeQueryHandler(IWhatBugDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response<GetEditPrioritySchemeQueryResult>> Handle(GetEditPrioritySchemeQuery request, CancellationToken cancellationToken)
        {
            var dto = await _context.PrioritySchemes
                .Where(s => s.Id == request.Id)
                .ProjectTo<GetEditPrioritySchemeQueryResult>(_mapper.ConfigurationProvider)
                .FirstAsync();

            dto.Priorities = await _context.Priorities
                .OrderBy(p => p.Order)
                .ProjectTo<PriorityDTO>(_mapper.ConfigurationProvider).ToListAsync();

            return Response<GetEditPrioritySchemeQueryResult>.Success(dto);
        }
    }
}