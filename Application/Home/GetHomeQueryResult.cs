using System;
using System.Collections.Generic;

namespace WhatBug.Application.Home
{
    public class GetHomeQueryResult
    {
        public IList<ProjectDto> Projects { get; set; }
        public IList<IssueDTO> ToDoIssues { get; set; }
        public IList<IssueDTO> InProgressIssues { get; set; }
    }

    public class ProjectDto
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public int CreatorId { get; set; }
        public string CreatorEmail { get; set; }
        public string CreatorName { get; set; }
        public int ProgressPercent { get; set; }
    }

    public class IssueDTO
    {
        public string Id { get; set; }
        public string Summary { get; set; }
        public int ProjectId { get; set; }
        public string Icon { get; set; }
        public string IconColor { get; set; }
        public string Priority { get; set; }
    }
}
