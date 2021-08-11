using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatBug.Common.Mapping;
using WhatBug.Domain.Entities.Priorities;

namespace WhatBug.Application.Priorities.Queries.GetEditPriority
{
    public class EditPriorityDTO : IMapFrom<Priority>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
        public int IconId { get; set; }
        public int ColorId { get; set; }
        public IList<IconDTO> Icons { get; set; }
        public IList<ColorDTO> Colors { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Priority, EditPriorityDTO>()
                .ForMember(d => d.IconId, opt => opt.MapFrom(s => s.ColorIcon.Icon.Id))
                .ForMember(d => d.ColorId, opt => opt.MapFrom(s => s.ColorIcon.Color.Id));
        }
    }
}
