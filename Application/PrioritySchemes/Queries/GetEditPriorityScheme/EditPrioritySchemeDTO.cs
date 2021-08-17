using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatBug.Common.Mapping;
using WhatBug.Domain.Entities;

namespace WhatBug.Application.PrioritySchemes.Queries.GetEditPriorityScheme
{
    public class EditPrioritySchemeDTO : IMapFrom<PriorityScheme>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IList<int> PriorityIds { get; set; }
        public IList<PriorityDTO> Priorities { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<PriorityScheme, EditPrioritySchemeDTO>()
                .ForMember(d => d.PriorityIds, opt => opt.MapFrom(s => s.Priorities.Select(p => p.Id)));
        }
    }
}
