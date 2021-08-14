using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatBug.Application.PermissionSchemes.Commands.GrantRolePermissions
{
    public class GrantRolePermissionsCommand : IRequest
    {
        public int SchemeId { get; set; }
        public int RoleId { get; set; }
        public IEnumerable<int> PermissionIds { get; set; }
    }
}
