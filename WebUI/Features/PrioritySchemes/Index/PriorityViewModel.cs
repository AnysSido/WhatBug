using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatBug.Application.PrioritySchemes.Queries.GetPrioritySchemes;
using WhatBug.Common.Mapping;
using WhatBug.WebUI.Common;

namespace WhatBug.WebUI.Features.PrioritySchemes.Index
{
    public class PriorityViewModel : IMapFrom<PriorityDTO>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
        public string IconName { get; set; }
        public string IconColor { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<PriorityDTO, PriorityViewModel>()
                .ForMember(d => d.IconName, opt => opt.MapFrom<IconClassNameResolver, string>(s => s.IconName))
                .ForMember(d => d.IconColor, opt => opt.MapFrom(s => s.IconColor.ToLowerInvariant()));
        }
    }
}
