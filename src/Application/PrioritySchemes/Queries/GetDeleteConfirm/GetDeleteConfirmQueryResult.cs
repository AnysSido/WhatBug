using AutoMapper;
using WhatBug.Common.Mapping;
using WhatBug.Domain.Entities;

namespace WhatBug.Application.PrioritySchemes.Queries.GetDeleteConfirm
{
    public class GetDeleteConfirmQueryResult : IMapFrom<PriorityScheme>
    {
        public int SchemeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<PriorityScheme, GetDeleteConfirmQueryResult>()
                .ForMember(d => d.SchemeId, opt => opt.MapFrom(s => s.Id));
        }
    }
}