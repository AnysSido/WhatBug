using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using WhatBug.Common.Mapping;
using WhatBug.Domain.Entities;

namespace WhatBug.Application.PrioritySchemes.Queries.GetPrioritySchemes
{
    public class GetPrioritySchemesQueryResult
    {
        public IList<PrioritySchemeDTO> PrioritySchemes { get; set; }
    }

    public class PrioritySchemeDTO : IMapFrom<PriorityScheme>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDefault { get; set; }
        public IList<PriorityDTO> Priorities { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<PriorityScheme, PrioritySchemeDTO>()
                .ForMember(d => d.Priorities, opt => opt.MapFrom(s => s.Priorities.Select(p => p.Priority)));
        }
    }

    public class PriorityDTO : IMapFrom<Priority>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
        public string IconWebName { get; set; }
        public string ColorName { get; set; }
    }
}