using AutoMapper;
using System.Collections.Generic;
using WhatBug.Common.Mapping;
using WhatBug.Domain.Entities;

namespace WhatBug.Application.PermissionSchemes.Queries.GetSchemeRoles
{
    public class SchemeDTO : IMapFrom<PermissionScheme>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IList<RoleDTO> Roles { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<PermissionScheme, SchemeDTO>()
                .ForMember(s => s.Roles, opt => opt.Ignore());
        }
    }
}
