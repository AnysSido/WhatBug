using AutoMapper;
using System.Collections.Generic;
using WhatBug.Common.Mapping;
using WhatBug.Domain.Entities;

namespace WhatBug.Application.Projects.Queries.GetKanbanBoard
{
    public class GetKanbanBoardQueryResult : IMapFrom<Project>
    {
        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; }
        public IList<IssueStatusGroupDTO> IssueStatusGroups { get; set; } = new List<IssueStatusGroupDTO>();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Project, GetKanbanBoardQueryResult>()
                .ForMember(d => d.ProjectName, opt => opt.MapFrom(s => s.Name))
                .ForMember(d => d.ProjectDescription, opt => opt.MapFrom(s => s.Description));
        }
    }

    public class IssueStatusGroupDTO : IMapFrom<IssueStatus>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<IssueOverviewDTO> Issues { get; set; } = new List<IssueOverviewDTO>();
    }

    public class IssueOverviewDTO : IMapFrom<Issue>
    {
        public string Id { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public int IssueStatusId { get; set; }
        public string IssueStatusName { get; set; }
        public string IssueTypeIconColor { get; set; }
        public string IssueTypeIconName { get; set; }
        public string IssueTypeIconWebName { get; set; }
        public string PriorityIconColor { get; set; }
        public string PriorityIconName { get; set; }
        public string PriorityIconWebName { get; set; }
        public string AssigneeEmail { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Issue, IssueOverviewDTO>()
                .ForMember(d => d.IssueTypeIconColor, opt => opt.MapFrom(s => s.IssueType.Color.Name))
                .ForMember(d => d.PriorityIconColor, opt => opt.MapFrom(s => s.Priority.Color.Name));
        }
    }
}
