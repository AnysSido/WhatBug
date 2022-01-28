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

namespace WhatBug.Application.Priorities.Queries.GetCreatePriority
{
    [Authorize(Permissions.ManagePriorities)]
    public record GetCreatePriorityQuery : IQuery<Response<GetCreatePriorityQueryResult>>
    {
    }

    public class GetCreatePriorityQueryHandler : IRequestHandler<GetCreatePriorityQuery, Response<GetCreatePriorityQueryResult>>
    {
        private readonly IWhatBugDbContext _context;
        private readonly IMapper _mapper;

        public GetCreatePriorityQueryHandler(IWhatBugDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response<GetCreatePriorityQueryResult>> Handle(GetCreatePriorityQuery request, CancellationToken cancellationToken)
        {
            var dto = new GetCreatePriorityQueryResult
            {
                Colors = await _context.Colors.OrderBy(c => c.Id).ProjectTo<ColorDTO>(_mapper.ConfigurationProvider).ToListAsync(),
                Icons = await _mapper.ProjectTo<IconDTO>(_context.Icons).ToListAsync()
            };

            return Response<GetCreatePriorityQueryResult>.Success(dto);
        }
    }
}