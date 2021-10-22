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

namespace WhatBug.Application.Priorities.Queries.GetDeleteConfirm
{
    [Authorize(Permissions.ManagePriorities)]
    public record GetDeleteConfirmQuery : IQuery<Response<GetDeleteConfirmQueryResult>>
    {
        public int PriorityId { get; init; }
    }

    public class GetDeleteConfirmQueryHandler : IRequestHandler<GetDeleteConfirmQuery, Response<GetDeleteConfirmQueryResult>>
    {
        private readonly IWhatBugDbContext _context;
        private readonly IMapper _mapper;

        public GetDeleteConfirmQueryHandler(IWhatBugDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response<GetDeleteConfirmQueryResult>> Handle(GetDeleteConfirmQuery request, CancellationToken cancellationToken)
        {
            var dto = await _context.Priorities
                .Where(p => p.Id == request.PriorityId)
                .ProjectTo<GetDeleteConfirmQueryResult>(_mapper.ConfigurationProvider)
                .FirstAsync();

            return Response<GetDeleteConfirmQueryResult>.Success(dto);
        }
    }
}