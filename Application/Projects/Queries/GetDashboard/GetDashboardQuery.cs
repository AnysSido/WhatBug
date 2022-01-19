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
using WhatBug.Domain.Entities;

namespace WhatBug.Application.Projects.Queries.GetDashboard
{
    [Authorize(PermissionOperator.Or, Permissions.ViewProject, Permissions.ViewAllProjects)]
    public record GetDashboardQuery : IQuery<Response<GetDashboardQueryResult>>, IRequireProjectAuthorization
    {
        public int ProjectId { get; set; }
    }

    public class GetDashboardQueryHandler : IRequestHandler<GetDashboardQuery, Response<GetDashboardQueryResult>>
    {
        private readonly IWhatBugDbContext _context;

        public GetDashboardQueryHandler(IWhatBugDbContext context)
        {
            _context = context;
        }

        public async Task<Response<GetDashboardQueryResult>> Handle(GetDashboardQuery request, CancellationToken cancellationToken)
        {
            var project = await LoadProjectAsync(request.ProjectId);

            var issueStatuses = project.Issues
                .OrderBy(i => i.IssueStatus.Id)
                .GroupBy(i => i.IssueStatus)
                .Select(group => new IssueStatusDTO
                {
                    Name = group.Key.Name,
                    IssueCount = group.Count()
                });

            var issuePriorities = project.Issues
                .GroupBy(i => i.Priority)
                .OrderBy(group => group.First().Priority.Order)
                .Select(group => new IssuePriorityDTO
                {
                    Name = group.Key.Name,
                    IssueCount = group.Count(),
                    PriorityColorName = group.First().Priority.Color.Name
                });

            var issueTypes = project.Issues
                .GroupBy(i => i.IssueType)
                .Select(group => new IssueTypeDTO
                {
                    Name = group.Key.Name,
                    IssueCount = group.Count(),
                    IssueTypeColorName = group.First().IssueType.Color.Name
                });

            var projectMembers = project.RoleUsers
                .DistinctBy(u => u.UserId)
                .Select(ru => new ProjectMemberDTO
                {
                    Id = ru.User.Id,
                    Email = ru.User.Email,
                    Name = $"{ru.User.FirstName} {ru.User.Surname}",
                    AssignedIssueCount = project.Issues.Where(i => i.AssigneeId == ru.UserId).Count()
                });

            var recentIssues = project.Issues
                .OrderByDescending(i => i.Created)
                .Take(5)
                .Select(i => new IssueDTO
                {
                    Id = i.Id,
                    Summary = i.Summary,
                    AssigneeId = i.Assignee.Id,
                    AssigneeEmail = i.Assignee.Email,
                    AssigneeFullName = $"{i.Assignee.FirstName} {i.Assignee.Surname}",
                    Priority = i.Priority.Name,
                    Icon = i.Priority.Icon.WebName,
                    IconColor = i.Priority.Color.Name,
                    Status = i.IssueStatus.Name
                });

            var recentComments = project.Issues
                .SelectMany(i => i.Comments)
                .OrderByDescending(c => c.Timestamp)
                .Take(5)
                .Select(c => new IssueCommentDTO
                {
                    Author = $"{c.Author.FirstName} {c.Author.Surname}",
                    Email = c.Author.Email,
                    IssueId = c.IssueId,
                    Content = c.Content
                });

            var totalIssues = project.Issues.Count();
            var issuesCompleted = project.Issues.Where(i => i.IssueStatus.Id == IssueStatuses.Done.Id).Count();

            var dto = new GetDashboardQueryResult
            {
                ProjectId = project.Id,
                ProjectName = project.Name,
                TotalIssues = totalIssues,
                RemainingIssues = totalIssues - issuesCompleted,
                IssuesCompleted = issuesCompleted,
                IssuesCompletedPercent = (int)Math.Round((double)issuesCompleted / totalIssues * 100),
                IssueStatuses = issueStatuses.ToList(),
                IssuePriorities = issuePriorities.ToList(),
                IssueTypes = issueTypes.ToList(),
                ProjectMembers = projectMembers.ToList(),
                RecentIssues = recentIssues.ToList(),
                RecentComments = recentComments.ToList()
            };

            return Response<GetDashboardQueryResult>.Success(dto);
        }

        private async Task<Project> LoadProjectAsync(int projectId)
        {
            /* Tracking is disabled by default for WhatBug queries. Tracking is required here to
             * load all required data from multiple queries into the project object.
             */
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;

            // Pre-load all required data for the dashboard into the change tracker instead of using one giant query.
            await _context.Issues.Include(i => i.IssueStatus).Include(i => i.IssueType).Where(i => i.ProjectId == projectId).ToListAsync();
            await _context.Issues.Include(i => i.Assignee).Where(i => i.ProjectId == projectId).ToListAsync();
            await _context.Priorities.Include(p => p.Icon).Include(p => p.Color).ToListAsync();
            await _context.IssueTypes.Include(t => t.Color).ToListAsync();
            await _context.ProjectRoleUsers.Include(r => r.Role).Include(u => u.User).Where(p => p.ProjectId == projectId).ToListAsync();
            await _context.Issues.Include(i => i.Comments).ThenInclude(c => c.Author).Where(i => i.ProjectId == projectId).ToListAsync();

            return await _context.Projects.Where(p => p.Id == projectId).FirstOrDefaultAsync();
        }
    }
}
