using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using WhatBug.Common.Mapping;
using WhatBug.Domain.Entities;

namespace WhatBug.Application.PrioritySchemes.Queries.GetEditPriorityScheme
{
    public class GetEditPrioritySchemeQueryResult : IMapFrom<PriorityScheme>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDefault { get; set; }
        public IList<int> PriorityIds { get; set; }
        public IList<PriorityDTO> Priorities { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<PriorityScheme, GetEditPrioritySchemeQueryResult>()
                .ForMember(d => d.PriorityIds, opt => opt.MapFrom(s => s.Priorities.Select(p => p.PriorityId)))
                .ForMember(d => d.Priorities, opt => opt.Ignore());
        }
    }

    public class PriorityDTO : IMapFrom<Priority>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string IconWebName { get; set; }
        public string ColorName { get; set; }
    }
}