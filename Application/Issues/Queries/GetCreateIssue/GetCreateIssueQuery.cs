using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Authorization;
using WhatBug.Application.Common.Exceptions;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Application.Common.MediatR;
using WhatBug.Application.Common.Security;
using WhatBug.Domain.Data;

namespace WhatBug.Application.Issues.Queries.GetCreateIssue
{
    [Authorize]
    public record GetCreateIssueQuery : IQuery<Response<GetCreateIssueQueryResult>>
    {
        public int? ProjectId { get; set; }
    }

    public class GetCreateIssueQueryHandler : IRequestHandler<GetCreateIssueQuery, Response<GetCreateIssueQueryResult>>
    {
        private readonly IWhatBugDbContext _context;
        private readonly IMapper _mapper;
        private readonly IAuthorizationManager _authManager;

        public GetCreateIssueQueryHandler(IWhatBugDbContext context, IMapper mapper, IAuthorizationManager authManager)
        {
            _context = context;
            _mapper = mapper;
            _authManager = authManager;
        }

        public async Task<Response<GetCreateIssueQueryResult>> Handle(GetCreateIssueQuery request, CancellationToken cancellationToken)
        {
            var projectSummaries = await GetProjectsAsync();

            if (projectSummaries.Count == 0)
                throw new AccessDeniedException("No access to any projects.");

            var project = await _mapper.ProjectTo<ProjectDTO>(_context.Projects)
                .FirstOrDefaultAsync(p => p.Id == request.ProjectId.GetValueOrDefault(projectSummaries.First().Id));

            if (project == null)
                throw new AccessDeniedException();

            var projectUsers = await _context.ProjectRoleUsers
                .Where(p => p.ProjectId == project.Id)
                .Select(u => u.User)
                .ProjectTo<UserDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();

            projectUsers = projectUsers
                .GroupBy(u => u.Id)
                .Select(group => group.First())
                .ToList();

            var issueTypes = await _mapper.ProjectTo<IssueTypeDTO>(_context.IssueTypes).ToListAsync();

            var dto = new GetCreateIssueQueryResult
            {
                Project = project,
                Projects = projectSummaries,
                IssueTypes = issueTypes,
                AssignableUsers = projectUsers,
                ReportingUsers = projectUsers
            };

            return Response<GetCreateIssueQueryResult>.Success(dto);
        }

        private async Task<List<ProjectSummaryDTO>> GetProjectsAsync()
        {
            if (await _authManager.HasPermission(Permissions.ViewAllProjects))
                return await _mapper.ProjectTo<ProjectSummaryDTO>(_context.Projects).ToListAsync();

            var projects = await _authManager.GetProjectsWithPermissionAsync(Permissions.ViewProject);

            return await _context.Projects.Where(p => projects.Contains(p.Id)).ProjectTo<ProjectSummaryDTO>(_mapper.ConfigurationProvider).ToListAsync();
        }
    }
}
