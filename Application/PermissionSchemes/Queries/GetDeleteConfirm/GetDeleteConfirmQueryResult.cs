using AutoMapper;
using WhatBug.Common.Mapping;
using WhatBug.Domain.Entities;

namespace WhatBug.Application.PermissionSchemes.Queries.GetDeleteConfirm
{
    public class GetDeleteConfirmQueryResult : IMapFrom<PermissionScheme>
    {
        public int SchemeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<PermissionScheme, GetDeleteConfirmQueryResult>()
                .ForMember(d => d.SchemeId, opt => opt.MapFrom(s => s.Id));
        }
    }
}