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

namespace WhatBug.Application.PrioritySchemes.Queries.GetPrioritySchemes
{
    [Authorize(Permissions.ManagePrioritySchemes)]
    public record GetPrioritySchemesQuery : IQuery<Response<GetPrioritySchemesQueryResult>>
    {
    }

    public class GetPrioritySchemesQueryHandler : IRequestHandler<GetPrioritySchemesQuery, Response<GetPrioritySchemesQueryResult>>
    {
        private readonly IWhatBugDbContext _context;
        private readonly IMapper _mapper;

        public GetPrioritySchemesQueryHandler(IWhatBugDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response<GetPrioritySchemesQueryResult>> Handle(GetPrioritySchemesQuery request, CancellationToken cancellationToken)
        {
            var schemes = await _context.PrioritySchemes
                .ProjectTo<PrioritySchemeDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();

            schemes.ForEach(scheme => scheme.Priorities = scheme.Priorities.OrderBy(p => p.Order).ToList());

            var dto = new GetPrioritySchemesQueryResult
            {
                PrioritySchemes = schemes
            };

            return Response<GetPrioritySchemesQueryResult>.Success(dto);
        }
    }
}