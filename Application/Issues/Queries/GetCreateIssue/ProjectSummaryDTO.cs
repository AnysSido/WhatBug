using WhatBug.Common.Mapping;
using WhatBug.Domain.Entities;

namespace WhatBug.Application.Issues.Queries.GetCreateIssue
{
    public class ProjectSummaryDTO : IMapFrom<Project>
    {
        public int Id {get;set;}
        public string Name { get; set; }
    }
}