using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatBug.WebUI.ViewModels.Permissions;

namespace WhatBug.WebUI.ViewModels.User
{
    public class UserWithPermissionsViewModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public IList<PermissionViewModel> Permissions { get; set; } = new List<PermissionViewModel>();
    }
}
