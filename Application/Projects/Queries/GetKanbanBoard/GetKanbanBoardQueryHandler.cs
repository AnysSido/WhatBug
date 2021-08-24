using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;

namespace WhatBug.Application.Projects.Queries.GetKanbanBoard
{
    public class GetKanbanBoardQueryHandler : IRequestHandler<GetKanbanBoardQuery, KanbanBoardDTO>
    {
        private readonly IWhatBugDbContext _context;
        private readonly IMapper _mapper;

        public GetKanbanBoardQueryHandler(IWhatBugDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<KanbanBoardDTO> Handle(GetKanbanBoardQuery request, CancellationToken cancellationToken)
        {
            var dto = await _mapper.ProjectTo<KanbanBoardDTO>(_context.Projects.Where(p => p.Id == request.ProjectId)).FirstOrDefaultAsync();

            if (dto == null)
            {
                // TODO: Throw NotFoundException
            }

            var issueStatusGroups = await _mapper.ProjectTo<IssueStatusGroupDTO>(_context.IssueStatuses).ToListAsync();
            var issues = await _mapper.ProjectTo<IssueOverviewDTO>(_context.Issues.Where(i => i.ProjectId == request.ProjectId)).ToListAsync();

            issues.GroupBy(issue => issue.IssueStatusId).ToList()
                .ForEach(issueGroup => issueStatusGroups.First(status => status.Id == issueGroup.Key).Issues.AddRange(issueGroup));

            issueStatusGroups.Sort((a,b) => a.Id.CompareTo(b.Id));

            dto.IssueStatusGroups = issueStatusGroups;

            return dto;
        }
    }
}
