using System.Collections.Generic;
using WhatBug.Common.Mapping;
using WhatBug.Domain.Entities;

namespace WhatBug.Application.Projects.Queries.GetDashboard
{
    public class GetDashboardQueryResult : IMapFrom<Project>
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public int TotalIssues { get; set; }
        public int RemainingIssues { get; set; }
        public int IssuesCompleted { get; set; }
        public int IssuesCompletedPercent { get; set; }

        public IList<IssueStatusDTO> IssueStatuses { get; set; }
        public IList<IssuePriorityDTO> IssuePriorities { get; set; }
        public IList<IssueTypeDTO> IssueTypes { get; set; }
        public IList<ProjectMemberDTO> ProjectMembers { get; set; }
        public IList<IssueDTO> RecentIssues { get; set; }
        public IList<IssueCommentDTO> RecentComments { get; set; }
    }

    public class IssueDTO
    {
        public string Id { get; set; }
        public string Summary { get; set; }
        public int AssigneeId { get; set; }
        public string AssigneeFullName { get; set; }
        public string AssigneeEmail { get; set; }
        public string Icon { get; set; }
        public string IconColor { get; set; }
        public string Priority { get; set; }
        public string Status { get; set; }
    }

    public class IssueStatusDTO
    {
        public string Name { get; set; }
        public int IssueCount { get; set; }
    }

    public class IssuePriorityDTO
    {
        public string Name { get; set; }
        public int IssueCount { get; set; }
        public string PriorityColorName { get; set; }
    }

    public class IssueTypeDTO
    {
        public string Name { get; set; }
        public int IssueCount { get; set; }
        public string IssueTypeColorName { get; set; }
    }

    public class ProjectMemberDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int AssignedIssueCount { get; set; }
    }

    public class IssueCommentDTO
    {
        public string Author { get; set; }
        public string Email { get; set; }
        public string IssueId { get; set; }
        public string Content { get; set; }
    }
}
