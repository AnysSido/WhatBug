using AutoMapper;
using WhatBug.Application.Priorities.Queries.GetPriorities;
using WhatBug.Common.Mapping;
using WhatBug.WebUI.Common;

namespace WhatBug.WebUI.Features.Priorities.Index
{
    public class PriorityViewModel : IMapFrom<PriorityDTO>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
        public string PriorityIconColor { get; set; }
        public string PriorityIconName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<PriorityDTO, PriorityViewModel>()
                .ForMember(d => d.PriorityIconName, opt => opt.MapFrom<IconClassNameResolver, string>(s => s.PriorityIconName))
                .ForMember(d => d.PriorityIconColor, opt => opt.MapFrom(s => s.PriorityIconColor.ToLowerInvariant()));
        }
    }
}