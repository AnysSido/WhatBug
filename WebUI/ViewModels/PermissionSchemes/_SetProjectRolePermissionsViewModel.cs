using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatBug.WebUI.ViewModels.Admin;
using WhatBug.WebUI.ViewModels.Permissions;

namespace WhatBug.WebUI.ViewModels.PermissionSchemes
{
    public class _SetProjectRolePermissionsViewModel
    {
        [HiddenInput]
        public int SchemeId { get; set; }
        public string SchemeName { get; set; }

        [HiddenInput]
        public int ProjectRoleId { get; set; }
        public string ProjectRoleName { get; set; }

        public IList<GrantablePermissionViewModel> GrantablePermissions { get; set; } = new List<GrantablePermissionViewModel>();
        public List<int> GrantedPermissionIds => GrantablePermissions.Where(p => p.IsGranted).Select(p => p.Id).ToList();
    }
}
