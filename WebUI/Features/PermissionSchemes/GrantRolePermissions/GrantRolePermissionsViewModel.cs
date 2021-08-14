using System.Collections.Generic;
using System.Linq;
using WhatBug.Application.PermissionSchemes.Queries.GetGrantRolePermissions;
using WhatBug.Common.Mapping;

namespace WhatBug.WebUI.Features.PermissionSchemes.GrantRolePermissions
{
    public class GrantRolePermissionsViewModel : IMapFrom<GrantRolePermissionsDTO>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public IList<PermissionDTO> Permissions { get; set; }

        public IEnumerable<int> GetPermissionIds()
        {
            return Permissions.Where(p => p.IsGranted).Select(p => p.Id).ToList();
        }
    }
}
