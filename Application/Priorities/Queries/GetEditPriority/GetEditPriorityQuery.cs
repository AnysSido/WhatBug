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

namespace WhatBug.Application.Priorities.Queries.GetEditPriority
{
    [Authorize(Permissions.ManagePriorities)]
    public record GetEditPriorityQuery : IQuery<Response<GetEditPriorityQueryResult>>
    {
        public int Id { get; set; }
    }

    public class GetEditPriorityQueryHandler : IRequestHandler<GetEditPriorityQuery, Response<GetEditPriorityQueryResult>>
    {
        private readonly IWhatBugDbContext _context;
        private readonly IMapper _mapper;

        public GetEditPriorityQueryHandler(IWhatBugDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response<GetEditPriorityQueryResult>> Handle(GetEditPriorityQuery request, CancellationToken cancellationToken)
        {
            var dto = await _context.Priorities
                .Where(p => p.Id == request.Id)
                .ProjectTo<GetEditPriorityQueryResult>(_mapper.ConfigurationProvider)
                .FirstAsync();

            dto.Colors = await _context.Colors.OrderBy(c => c.Id).ProjectTo<ColorDTO>(_mapper.ConfigurationProvider).ToListAsync();
            dto.Icons = await _mapper.ProjectTo<IconDTO>(_context.Icons).ToListAsync();

            return Response<GetEditPriorityQueryResult>.Success(dto);
        }
    }
}