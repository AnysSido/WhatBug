using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using WhatBug.Common.Mapping;
using WhatBug.Domain.Entities;

namespace WhatBug.Application.PermissionSchemes.Queries.GetGrantRolePermissions
{
    public class GrantRolePermissionsDTO : IMapFrom<PermissionScheme>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public IList<PermissionDTO> Permissions { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<PermissionScheme, GrantRolePermissionsDTO>()
                .ForMember(d => d.Permissions, opt => opt.MapFrom(s => s.ProjectRolePermissions.Select(p => p.Permission)));
        }
    }
}
