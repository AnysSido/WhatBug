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

namespace WhatBug.Application.Projects.Queries.GetProjects
{
    public record GetProjectsQuery : IQuery<Response<GetProjectsQueryResult>>
    {
    }

    public partial class GetProjectsQueryHandler : IRequestHandler<GetProjectsQuery, Response<GetProjectsQueryResult>>
    {
        private readonly IWhatBugDbContext _context;

        public GetProjectsQueryHandler(IWhatBugDbContext context)
        {
            _context = context;
        }

        public async Task<Response<GetProjectsQueryResult>> Handle(GetProjectsQuery request, CancellationToken cancellationToken)
        {
            var projects = await _context.Projects
                .Include(p => p.Issues).ThenInclude(i => i.IssueStatus)
                .ToListAsync();

            var creators = await _context.Users.Where(u => projects.Select(p => p.CreatedBy).Contains(u.Id)).ToListAsync();

            var dto = new GetProjectsQueryResult
            {
                Projects = new List<ProjectDto>()
            };

            foreach (var project in projects)
            {
                var totalIssueCount = project.Issues.Count();
                var completedIssueCount = project.Issues.Where(i => i.IssueStatus.Name == "Done").Count(); // TODO: Remove magic string
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
