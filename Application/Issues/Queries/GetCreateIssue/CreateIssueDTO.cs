using System.Collections.Generic;

namespace WhatBug.Application.Issues.Queries.GetCreateIssue
{
    public class CreateIssueDTO
    {
        public string Summary { get; set; }
        public string Description { get; set; }
        public ProjectDTO Project { get; set; }
        public IList<ProjectSummaryDTO> Projects { get; set; }
        public IList<IssueTypeDTO> IssueTypes { get; set; }
        public IList<UserDTO> AssignableUsers { get; set; }
        public IList<UserDTO> ReportingUsers { get; set; }
    }
}
