using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using WhatBug.Common.Mapping;
using WhatBug.Domain.Entities;

namespace WhatBug.Application.Users.Queries.GetUserPermissions
{
    public class UserPermissionsDTO : IMapFrom<User>
    {
        public IList<PermissionDTO> Permissions { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, UserPermissionsDTO>()
                .ForMember(d => d.Permissions, opt => opt.MapFrom(s => s.UserPermissions.Select(p => p.Permission)));
        }
    }
}
