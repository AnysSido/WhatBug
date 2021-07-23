using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatBug.Application.DTOs.Users;

namespace WhatBug.WebUI.ViewModels.Permissions
{
    public class PermissionsGlobalEditViewModel
    {
        public UserWithPermissionsDTO UserWithPermissions { get; set; }
        public List<GrantablePermission> GrantablePermissions { get; set; }
        public List<PermissionViewModel> AllPermissions { get; set; }
        public List<int> GrantedPermissionIds => GrantablePermissions
            .Where(p =>p.IsGranted)
            .Select(p => p.PermissionId)
            .ToList();

        [Obsolete("For model binding only", true)]
        public PermissionsGlobalEditViewModel(){ }

        public PermissionsGlobalEditViewModel(List<PermissionViewModel> permissions, UserWithPermissionsDTO user)
        {
            UserWithPermissions = user;
            AllPermissions = permissions;
            GrantablePermissions = permissions.Select(p => new GrantablePermission()
            {
                PermissionId = p.Id,
                IsGranted = UserWithPermissions.Permissions.Any(up => up.Id == p.Id)
            }).ToList();
        }

        public class GrantablePermission
        {
            public int PermissionId { get; set; }
            public bool IsGranted { get; set; }
        }
    }
}
