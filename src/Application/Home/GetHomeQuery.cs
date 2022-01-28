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

namespace WhatBug.Application.Home
{
    [Authorize]
    public record GetHomeQuery : IQuery<Response<GetHomeQueryResult>>
    {
    }

    public class GetHomeQueryHandler : IRequestHandler<GetHomeQuery, Response<GetHomeQueryResult>>
    {
        private readonly IWhatBugDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public GetHomeQueryHandler(IWhatBugDbContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<Response<GetHomeQueryResult>> Handle(GetHomeQuery request, CancellationToken cancellationToken)
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

            var dto = new GetHomeQueryResult
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

            var toDoIssues = await _context.Issues
                .Where(i => i.AssigneeId == _currentUserService.Id)
                .Where(i => i.IssueStatus.Id == IssueStatuses.ToDo.Id)
                .Select(i => new IssueDTO
                {
                    Id = i.Id,
                    ProjectId = i.ProjectId,
                    Summary = i.Summary,
                    Priority = i.Priority.Name,
                    Icon = i.Priority.Icon.WebName,
                    IconColor = i.Priority.Color.Name,
                })
                .ToListAsync();

            var inProgressIssues = await _context.Issues
                .Where(i => i.AssigneeId == _currentUserService.Id)
                .Where(i => i.IssueStatus.Id == IssueStatuses.InProgress.Id)
                .Select(i => new IssueDTO
                {
                    Id = i.Id,
                    ProjectId = i.ProjectId,
                    Summary = i.Summary,
                    Priority = i.Priority.Name,
                    Icon = i.Priority.Icon.WebName,
                    IconColor = i.Priority.Color.Name,
                })
                .ToListAsync();

            dto.ToDoIssues = toDoIssues;
            dto.InProgressIssues = inProgressIssues;

            return Response<GetHomeQueryResult>.Success(dto);
        }
    }
}
