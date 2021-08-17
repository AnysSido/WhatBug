using WhatBug.Common.Mapping;
using WhatBug.Domain.Entities;

namespace WhatBug.Application.Permissions.Queries.GetGlobalPermissions
{
    public class PermissionDTO : IMapFrom<Permission>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}