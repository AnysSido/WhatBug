using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatBug.WebUI.ViewModels.User;

namespace WhatBug.WebUI.ViewModels.GlobalPermissions
{
    public class GlobalPermissionIndexViewModel
    {
        public IList<UserWithPermissionsViewModel> Users { get; set; }
    }
}
