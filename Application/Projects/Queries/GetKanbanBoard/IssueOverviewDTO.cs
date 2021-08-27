using AutoMapper;
using WhatBug.Common.Mapping;
using WhatBug.Domain.Entities;

namespace WhatBug.Application.Projects.Queries.GetKanbanBoard
{
    public class IssueOverviewDTO : IMapFrom<Issue>
    {
        public string Id { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public int IssueStatusId { get; set; }
        public string IssueStatusName { get; set; }
        public string IssueTypeIconColor { get; set; }
        public string IssueTypeIconName { get; set; }
        public string PriorityIconColor { get; set; }
        public string PriorityIconName { get; set; }
        public string AssigneeEmail { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Issue, IssueOverviewDTO>()
                .ForMember(d => d.IssueTypeIconColor, opt => opt.MapFrom(s => s.IssueType.Color.Name))
                .ForMember(d => d.PriorityIconColor, opt => opt.MapFrom(s => s.Priority.Color.Name));
        }
    }
}
