using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatBug.Application.DTOs.PermissionSchemes
{
    public class SetProjectRolePermissionsDTO
    {
        public int SchemeId { get; set; }
        public int ProjectRoleId { get; set; }
        public IEnumerable<int> GrantedPermissionIds { get; set; }
    }
}
