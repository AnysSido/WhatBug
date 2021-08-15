using AutoMapper;
using WhatBug.Common.Mapping;
using WhatBug.Domain.Entities.Priorities;

namespace WhatBug.Application.PrioritySchemes.Queries.GetPrioritySchemes
{
    public class PriorityDTO : IMapFrom<Priority>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
        public string IconName { get; set; }
        public string IconColor { get; set; }
        
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Priority, PriorityDTO>()
                .ForMember(d => d.IconName, opt => opt.MapFrom(s => s.Icon.Name))
                .ForMember(d => d.IconColor, opt => opt.MapFrom(s => s.Color.Name));
        }
    }
}