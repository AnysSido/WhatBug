using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Application.Common.MediatR;
using WhatBug.Application.Common.Security;
using WhatBug.Domain.Data;

namespace WhatBug.Application.Issues.Queries.GetIssueDetail
{
    [Authorize(Permissions.ViewProject)]
    public record GetIssueDetailQuery : IQuery<Response<IssueDetailDTO>>, IRequireIssueAuthorization
    {
        public string IssueId { get; set; }
    }

    public class GetIssueDetailQueryHandler : IRequestHandler<GetIssueDetailQuery, Response<IssueDetailDTO>>
    {
        private readonly IWhatBugDbContext _context;
        private readonly IMapper _mapper;

        public GetIssueDetailQueryHandler(IWhatBugDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response<IssueDetailDTO>> Handle(GetIssueDetailQuery request, CancellationToken cancellationToken)
        {
            var dto = await _context.Issues
                .Where(i => i.Id == request.IssueId)
                .ProjectTo<IssueDetailDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            dto.IssueTypes = await  _context.IssueTypes
                .ProjectTo<IssueTypeDTO>(_mapper.ConfigurationProvider)
                .OrderBy(i => i.Id)
                .ToListAsync();

            dto.Priorities = await _context.Priorities
                .ProjectTo<PriorityDTO>(_mapper.ConfigurationProvider)
                .OrderBy(p => !p.IsDefault).ThenBy(p => p.Order)
                .ToListAsync();

            return Response<IssueDetailDTO>.Success(dto);
        }
    }
}
