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

namespace WhatBug.Application.PrioritySchemes.Queries.GetDeleteConfirm
{
    [Authorize(Permissions.ManagePrioritySchemes)]
    public record GetDeleteConfirmQuery : IQuery<Response<GetDeleteConfirmQueryResult>>
    {
        public int SchemeId { get; set; }
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
            var dto = await _context.PrioritySchemes
                .Where(s => s.Id == request.SchemeId)
                .ProjectTo<GetDeleteConfirmQueryResult>(_mapper.ConfigurationProvider)
                .FirstAsync();

            return Response<GetDeleteConfirmQueryResult>.Success(dto);
        }
    }
}