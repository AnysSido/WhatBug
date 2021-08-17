using AutoMapper;
using WhatBug.Common.Mapping;
using WhatBug.Domain.Entities.Priorities;

namespace WhatBug.Application.Priorities.Queries.GetPriorities
{
    public class PriorityDTO : IMapFrom<Priority>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
        public string PriorityIconColor { get; set; }
        public string PriorityIconName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Priority, PriorityDTO>()
                .ForMember(d => d.PriorityIconColor, opt => opt.MapFrom(s => s.Color.Name))
                .ForMember(d => d.PriorityIconName, opt => opt.MapFrom(s => s.Icon.Name));
        }
    }
}