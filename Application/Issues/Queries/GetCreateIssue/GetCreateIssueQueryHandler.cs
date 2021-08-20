using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;

namespace WhatBug.Application.Issues.Queries.GetCreateIssue
{
    public class GetCreateIssueQueryHandler : IRequestHandler<GetCreateIssueQuery, CreateIssueDTO>
    {
        private readonly IWhatBugDbContext _context;
        private readonly IMapper _mapper;

        public GetCreateIssueQueryHandler(IWhatBugDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CreateIssueDTO> Handle(GetCreateIssueQuery request, CancellationToken cancellationToken)
        {
            // TODO: Check permissions
            // TODO: This should only load projects etc that current user can access
            var projects = await _mapper.ProjectTo<ProjectSummaryDTO>(_context.Projects).ToListAsync();
            var projectId = request.ProjectId.GetValueOrDefault(projects.First().Id);
            var project = await _mapper.ProjectTo<ProjectDTO>(_context.Projects).FirstOrDefaultAsync(p => p.Id == projectId); // TODO: Handle no projects

            var issueTypes = await _mapper.ProjectTo<IssueTypeDTO>(_context.IssueTypes).ToListAsync();
            var assignableUsers = await _mapper.ProjectTo<UserDTO>(_context.Users).ToListAsync();
            var reportingUsers = await _mapper.ProjectTo<UserDTO>(_context.Users).ToListAsync();

            var dto = new CreateIssueDTO
            {
                Project = project,
                Projects = projects,
                IssueTypes = issueTypes,
                AssignableUsers = assignableUsers,
                ReportingUsers = reportingUsers
            };

            return dto;
        }
    }
}
