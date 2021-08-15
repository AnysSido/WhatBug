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

        public void Mapping(Profile profile)
        {
            profile.CreateMap<IssueType, IssueTypeDTO>()
                .ForMember(d => d.ColorName, opt => opt.MapFrom(s => s.ColorIcon.Color.Name))
                .ForMember(d => d.IconName, opt => opt.MapFrom(s => s.ColorIcon.Icon.Name));
        }
    }
}