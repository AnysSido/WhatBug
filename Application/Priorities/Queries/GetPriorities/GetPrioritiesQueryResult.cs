using AutoMapper;
using System.Collections.Generic;
using WhatBug.Common.Mapping;
using WhatBug.Domain.Entities;

namespace WhatBug.Application.Priorities.Queries.GetPriorities
{
    public class GetPrioritiesQueryResult
    {
        public IList<PriorityDTO> Priorities { get; set; }
    }

    public class PriorityDTO : IMapFrom<Priority>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
        public string Color { get; set; }
        public string Icon { get; set; }
        public string IconWebName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Priority, PriorityDTO>()
                .ForMember(d => d.Color, opt => opt.MapFrom(s => s.Color.Name))
                .ForMember(d => d.Icon, opt => opt.MapFrom(s => s.Icon.Name));
        }
    }
}