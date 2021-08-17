using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using WhatBug.Common.Mapping;
using WhatBug.Domain.Entities;

namespace WhatBug.Application.Permissions.Queries.GetGlobalPermissions
{
    public class UserDTO : IMapFrom<User>
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public IList<PermissionDTO> Permissions { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, UserDTO>()
                .ForMember(d => d.Permissions, opt => opt.MapFrom(s => s.UserPermissions.Select(p => p.Permission)));
        }
    }
}