using WhatBug.Common.Mapping;
using WhatBug.Domain.Entities;

namespace WhatBug.WebUI.Features.PermissionSchemes.GrantRolePermissions
{
    public class PermissionViewModel : IMapFrom<Permission>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
