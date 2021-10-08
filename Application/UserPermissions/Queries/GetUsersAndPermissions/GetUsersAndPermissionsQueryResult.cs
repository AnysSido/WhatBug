using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using WhatBug.Common.Mapping;
using WhatBug.Domain.Entities;

namespace WhatBug.Application.UserPermissions.Queries.GetUsersAndPermissions
{
    public class GetUsersAndPermissionsQueryResult
    {
        public IList<UserDTO> Users { get; set; }
    }

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

    public class PermissionDTO : IMapFrom<Permission>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
