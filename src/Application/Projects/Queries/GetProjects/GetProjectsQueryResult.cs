using System;
using System.Collections.Generic;

namespace WhatBug.Application.Projects.Queries.GetProjects
{
    public class GetProjectsQueryResult
    {
        public IList<ProjectDto> Projects { get; set; }
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
}
