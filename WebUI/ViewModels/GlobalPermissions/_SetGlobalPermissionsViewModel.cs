using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatBug.WebUI.ViewModels.Permissions;

namespace WhatBug.WebUI.ViewModels.GlobalPermissions
{
    public class _SetGlobalPermissionsViewModel
    {
        [HiddenInput]
        public int UserId { get; set; }
        public string Username { get; set; }

        public IList<GrantablePermissionViewModel> GrantablePermissions { get; set; } = new List<GrantablePermissionViewModel>();
        public List<int> GrantedPermissionIds => GrantablePermissions.Where(p => p.IsGranted).Select(p => p.Id).ToList();
    }
}
