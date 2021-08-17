using AutoMapper;
using WhatBug.Common.Mapping;
using WhatBug.Domain.Entities;

namespace WhatBug.Application.Issues.Queries.GetIssueDetail
{
    public class IssueDetailDTO : IMapFrom<Issue>
    {
        public string Id { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        
        public string PriorityName { get; set; }
        public string PriorityIconName { get; set; }
        public string PriorityIconColor { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Issue, IssueDetailDTO>()
                .ForMember(d => d.PriorityIconColor, opt => opt.MapFrom(s => s.Priority.Color.Name));
        }
    }
}
