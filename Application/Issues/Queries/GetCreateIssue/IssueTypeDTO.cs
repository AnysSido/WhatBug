using AutoMapper;
using WhatBug.Common.Mapping;
using WhatBug.Domain.Entities;

namespace WhatBug.Application.Issues.Queries.GetCreateIssue
{
    public class IssueTypeDTO : IMapFrom<IssueType>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ColorName { get; set; }
        public string IconName { get; set; }
    }
}