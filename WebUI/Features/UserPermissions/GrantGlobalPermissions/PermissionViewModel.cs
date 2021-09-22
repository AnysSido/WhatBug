using WhatBug.Application.UserPermissions.Queries.GetGrantGlobalPermissions;
using WhatBug.Common.Mapping;

namespace WhatBug.WebUI.Features.Permissions.GrantGlobalPermissions
{
    public class PermissionViewModel : IMapFrom<PermissionDTO>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsGranted { get; set; }
    }
}