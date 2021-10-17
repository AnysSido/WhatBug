using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Application.Common.MediatR;
using WhatBug.Application.Common.Security;
using WhatBug.Domain.Data;

namespace WhatBug.Application.Priorities.Queries.GetPriorities
{
    [Authorize(Permissions.ManagePriorities)]
    public record GetPrioritiesQuery : IQuery<Response<GetPrioritiesQueryResult>>
    {
    }

    public class GetPrioritiesQueryHandler : IRequestHandler<GetPrioritiesQuery, Response<GetPrioritiesQueryResult>>
    {
        private readonly IWhatBugDbContext _context;
        private readonly IMapper _mapper;

        public GetPrioritiesQueryHandler(IWhatBugDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response<GetPrioritiesQueryResult>> Handle(GetPrioritiesQuery request, CancellationToken cancellationToken)
        {
            var priorities = await _mapper.ProjectTo<PriorityDTO>(_context.Priorities).OrderBy(p => p.Order).ToListAsync();
            return Response<GetPrioritiesQueryResult>.Success(new GetPrioritiesQueryResult { Priorities = priorities });
        }
    }
}