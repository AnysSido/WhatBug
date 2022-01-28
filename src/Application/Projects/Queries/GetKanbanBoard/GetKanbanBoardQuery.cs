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

namespace WhatBug.Application.Projects.Queries.GetKanbanBoard
{
    [Authorize(Permissions.ViewProject)]
    public record GetKanbanBoardQuery : IQuery<Response<GetKanbanBoardQueryResult>>, IRequireProjectAuthorization
    {
        public int ProjectId { get; init; }
    }

    public class GetKanbanBoardQueryHandler : IRequestHandler<GetKanbanBoardQuery, Response<GetKanbanBoardQueryResult>>
    {
        private readonly IWhatBugDbContext _context;
        private readonly IMapper _mapper;

        public GetKanbanBoardQueryHandler(IWhatBugDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response<GetKanbanBoardQueryResult>> Handle(GetKanbanBoardQuery request, CancellationToken cancellationToken)
        {
            var dto = await _context.Projects
                .Where(p => p.Id == request.ProjectId)
                .ProjectTo<GetKanbanBoardQueryResult>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            var issueStatusGroups = await _context.IssueStatuses
                .ProjectTo<IssueStatusGroupDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();

            var issues = await _context.Issues
                .Where(i => i.ProjectId == request.ProjectId)
                .ProjectTo<IssueOverviewDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();

            issues.GroupBy(issue => issue.IssueStatusId).ToList()
                .ForEach(issueGroup => issueStatusGroups.First(status => status.Id == issueGroup.Key).Issues.AddRange(issueGroup));

            issueStatusGroups.Sort((a, b) => a.Id.CompareTo(b.Id));

            dto.IssueStatusGroups = issueStatusGroups;

            return Response<GetKanbanBoardQueryResult>.Success(dto);
        }
    }
}
