using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Application.Common.MediatR;
using WhatBug.Application.Common.Security;
using WhatBug.Domain.Data;

namespace WhatBug.Application.Projects.Queries.GetProjects
{
    [Authorize]
    public record GetProjectsQuery : IQuery<Response<GetProjectsQueryResult>>
    {
    }

    public partial class GetProjectsQueryHandler : IRequestHandler<GetProjectsQuery, Response<GetProjectsQueryResult>>
    {
        private readonly IWhatBugDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public GetProjectsQueryHandler(IWhatBugDbContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<Response<GetProjectsQueryResult>> Handle(GetProjectsQuery request, CancellationToken cancellationToken)
        {

            var myProjectIds = await _context.ProjectRoleUsers
                .Where(u => u.UserId == _currentUserService.Id)
                .Select(p => p.ProjectId)
                .Distinct()
                .ToListAsync();

            var myProjects = await _context.Projects
                .Include(p => p.Issues).ThenInclude(i => i.IssueStatus)
                .Where(p => myProjectIds.Contains(p.Id))
                .ToListAsync();

            var creators = await _context.Users.Where(u => myProjects.Select(p => p.CreatedBy).Contains(u.Id)).ToListAsync();

            var dto = new GetProjectsQueryResult
            {
                Projects = new List<ProjectDto>()
            };

            foreach (var project in myProjects)
            {
                var totalIssueCount = project.Issues.Count();
                var completedIssueCount = project.Issues.Where(i => i.IssueStatus.Id == IssueStatuses.Done.Id).Count();
                var creator = creators.First(u => u.Id == project.CreatedBy);

                dto.Projects.Add(new ProjectDto
                {
                    Id = project.Id,
                    Key = project.Key,
                    Name = project.Name,
                    Description = project.Description,
                    Created = project.Created,
                    CreatorId = creator.Id,
                    CreatorEmail = creator.Email,
                    CreatorName = $"{creator.FirstName} {creator.Surname}",
                    ProgressPercent = (int)Math.Round((double)completedIssueCount / totalIssueCount * 100)
                });
            }

            return Response<GetProjectsQueryResult>.Success(dto);
        }
    }
}
