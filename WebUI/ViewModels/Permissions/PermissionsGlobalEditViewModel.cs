using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatBug.Application.DTOs.Users;

namespace WhatBug.WebUI.ViewModels.Permissions
{
    public class PermissionsGlobalEditViewModel
    {
        public UserWithPermissionsDTO userWithPermissions { get; set; }
        public List<PermissionViewModel> allPermissions { get; set; }
    }
}
