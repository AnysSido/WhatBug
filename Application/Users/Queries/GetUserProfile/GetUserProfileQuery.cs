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

namespace WhatBug.Application.Users.Queries.GetUserProfile
{
    public record GetUserProfileQuery : IQuery<Response<GetUserProfileQueryResult>>
    {
        public int UserId { get; set; }
    }

    public class GetUserProfileQueryHandler : IRequestHandler<GetUserProfileQuery, Response<GetUserProfileQueryResult>>
    {
        private readonly IWhatBugDbContext _context;

        public GetUserProfileQueryHandler(IWhatBugDbContext context)
        {
            _context = context;
        }

        public async Task<Response<GetUserProfileQueryResult>> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
        {
            await LoadData(request.UserId);

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == request.UserId);

            var userProjects = await _context.ProjectRoleUsers
                .Where(u => u.UserId == request.UserId)
                .Select(p => p.Project)
                .ToListAsync();

            var recentIssues = await _context.Issues
                .Where(i => userProjects.Select(p => p.Id).Contains(i.ProjectId))
                .Where(i => i.CreatedBy == request.UserId)
                .OrderByDescending(i => i.Created)
                .Take(10)
                .Select(i => new IssueDTO
                {
                    Id = i.Id,
                    ProjectId = i.ProjectId,
                    Summary = i.Summary,
                    AssigneeId = i.Assignee.Id,
                    AssigneeEmail = i.Assignee.Email,
                    AssigneeFullName = $"{i.Assignee.FirstName} {i.Assignee.Surname}",
                    Status = i.IssueStatus.Name,
                    Priority = i.Priority.Name,
                    Icon = i.Priority.Icon.WebName,
                    IconColor = i.Priority.Color.Name
                })
                .ToListAsync();

            var recentComments = await _context.IssueComments
                .Include(c => c.Issue)
                .Where(c => c.AuthorId == request.UserId)
                .OrderByDescending(c => c.Timestamp)
                .Take(10)
                .Select(c => new IssueCommentDTO
                {
                    Author = $"{c.Author.FirstName} {c.Author.Surname}",
                    Email = c.Author.Email,
                    IssueId = c.IssueId,
                    Content = c.Content,
                    ProjectId = c.Issue.ProjectId
                })
                .ToListAsync();

            var dto = new GetUserProfileQueryResult
            {
                FullName = $"{user.FirstName} {user.Surname}",
                Email = user.Email,
                RecentIssues = recentIssues,
                RecentComments = recentComments
            };

            return Response<GetUserProfileQueryResult>.Success(dto);
        }

        private async Task LoadData(int userId)
        {
            /* Tracking is disabled by default for WhatBug queries. Tracking is required here to
             * load all required data from multiple queries into the project object.
             */
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;

            // Pre-load required data for the profile into the change tracker instead of using one giant query.
            var userProjects = await _context.ProjectRoleUsers
                .Where(u => u.UserId == userId)
                .Include(p => p.Project)
                .Select(p => p.Project)
                .ToListAsync();

            await _context.Issues
                .Include(i => i.Priority).ThenInclude(i => i.Icon)
                .Include(i => i.Priority).ThenInclude(i => i.Color)
                .Where(i => userProjects.Select(p => p.Id).Contains(i.ProjectId)).ToListAsync();

            await _context.Issues
                .Include(i => i.IssueStatus)
                .Include(i => i.Assignee)
                .Where(i => userProjects.Select(p => p.Id).Contains(i.ProjectId)).ToListAsync();
        }
    }
}
