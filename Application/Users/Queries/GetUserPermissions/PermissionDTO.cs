using WhatBug.Common.Mapping;
using WhatBug.Domain.Entities;

namespace WhatBug.Application.Users.Queries.GetUserPermissions
{
    public class PermissionDTO : IMapFrom<Permission>
    {
        public string Name { get; set; }
    }
}