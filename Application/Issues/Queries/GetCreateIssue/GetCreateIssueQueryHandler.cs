using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;

namespace WhatBug.Application.Issues.Queries.GetCreateIssue
{
    public class GetCreateIssueQueryHandler : IRequestHandler<GetCreateIssueQuery, CreateIssueDTO>
    {
        private readonly IWhatBugDbContext _context;
        private readonly IAuthenticationProvider _authProvider;
        private readonly IMapper _mapper;

        public GetCreateIssueQueryHandler(IWhatBugDbContext context, IMapper mapper, IAuthenticationProvider authProvider)
        {
            _context = context;
            _mapper = mapper;
            _authProvider = authProvider;
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

            // TODO: Fix this. We should not be querying usernames one by one. This should be replaced with data
            // from the User table but right now the User table is empty so we are using username from the
            // authentication table.
            foreach (var user in assignableUsers)
                user.Username = await _authProvider.GetUsername(user.Id);
            foreach (var user in reportingUsers)
                user.Username = await _authProvider.GetUsername(user.Id);

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
