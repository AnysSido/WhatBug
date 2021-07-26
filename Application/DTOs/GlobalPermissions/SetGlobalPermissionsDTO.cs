using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatBug.Application.DTOs.GlobalPermissions
{
    public class SetGlobalPermissionsDTO
    {
        public int UserId { get; set; }
        public IEnumerable<int> GrantedPermissionIds { get; set; }
    }
}
