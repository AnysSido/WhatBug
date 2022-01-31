using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WhatBug.Application.Issues.Queries.GetCreateIssue;
using WhatBug.Common.Mapping;

namespace WhatBug.WebUI.Components.CreateIssue
{
    public class CreateIssueViewModel : IMapFrom<GetCreateIssueQueryResult>
    {
        public bool CreateAnother { get; set; }

        public string Summary { get; set; }

        public string Description { get; set; }

        [Display(Name = "Project")]
        public int ProjectId { get; set; }

        [Display(Name = "Issue Type")]
        public int IssueTypeId { get; set; }

        [Display(Name = "Priority")]
        public int PriorityId { get; set; }

        [Display(Name = "Assignee")]
        public int AssigneeId { get; set; }

        [Display(Name = "Reporter")]
        public int ReporterId { get; set; }

        public ProjectDTO Project { get; set; }

        public IList<ProjectSummaryDTO> Projects { get; set; }

        public IList<IssueTypeDTO> IssueTypes { get; set; }

        public IList<UserDTO> AssignableUsers { get; set; }

        public IList<UserDTO> ReportingUsers { get; set; }
    }
}
