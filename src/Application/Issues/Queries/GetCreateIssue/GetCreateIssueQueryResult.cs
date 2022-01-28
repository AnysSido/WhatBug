using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using WhatBug.Common.Mapping;
using WhatBug.Domain.Entities;

namespace WhatBug.Application.Issues.Queries.GetCreateIssue
{
    public class GetCreateIssueQueryResult
    {
        public string Summary { get; set; }
        public string Description { get; set; }
        public ProjectDTO Project { get; set; }
        public IList<ProjectSummaryDTO> Projects { get; set; }
        public IList<IssueTypeDTO> IssueTypes { get; set; }
        public IList<UserDTO> AssignableUsers { get; set; }
        public IList<UserDTO> ReportingUsers { get; set; }
    }

    public class IssueTypeDTO : IMapFrom<IssueType>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ColorName { get; set; }
        public string IconName { get; set; }
        public string IconWebName { get; set; }
    }

    public class PriorityDTO : IMapFrom<Priority>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ColorName { get; set; }
        public string IconName { get; set; }
        public string IconWebName { get; set; }
    }

    public class ProjectDTO : IMapFrom<Project>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<PriorityDTO> Priorities { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Project, ProjectDTO>()
                .ForMember(d => d.Priorities, opt => opt.MapFrom(s => s.PriorityScheme.Priorities.Select(p => p.Priority).OrderBy(p => p.Order)));
        }
    }

    public class ProjectSummaryDTO : IMapFrom<Project>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class UserDTO : IMapFrom<User>
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
    }
}
