using System.Collections.Generic;
using System.Linq;
using WhatBug.Application.UserPermissions.Queries.GetGrantGlobalPermissions;
using WhatBug.Common.Mapping;

namespace WhatBug.WebUI.Features.Permissions.GrantGlobalPermissions
{
    public class GrantGlobalPermissionsViewModel : IMapFrom<GrantGlobalPermissionsDTO>
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public IList<PermissionViewModel> Permissions { get; set; }

        public IEnumerable<int> GetPermissionIds()
        {
            return Permissions.Where(p => p.IsGranted).Select(p => p.Id).ToList();
        }
    }
}
