using AutoMapper;
using WhatBug.Application.Priorities.Queries.GetCreatePriority;
using WhatBug.Common.Mapping;
using WhatBug.WebUI.Common;

namespace WhatBug.WebUI.Features.Priorities.Create
{
    public class IconViewModel : IMapFrom<IconDTO>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<IconDTO, IconViewModel>()
                .ForMember(d => d.Name, opt => opt.MapFrom<IconClassNameResolver, string>(s => s.Name));
        }
    }
}