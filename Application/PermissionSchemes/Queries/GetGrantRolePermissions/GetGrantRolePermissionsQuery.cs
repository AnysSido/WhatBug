using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatBug.Application.PermissionSchemes.Queries.GetGrantRolePermissions
{
    public class GetGrantRolePermissionsQuery : IRequest<GrantRolePermissionsDTO>
    {
        public int SchemeId { get; set; }
        public int RoleId { get; set; }
        public IEnumerable<int> PermissionIds { get; set; }
    }
}
