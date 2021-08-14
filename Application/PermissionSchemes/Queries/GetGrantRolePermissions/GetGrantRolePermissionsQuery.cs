using MediatR;
using System.Collections.Generic;

namespace WhatBug.Application.PermissionSchemes.Queries.GetGrantRolePermissions
{
    public class GetGrantRolePermissionsQuery : IRequest<GrantRolePermissionsDTO>
    {
        public int SchemeId { get; set; }
        public int RoleId { get; set; }
        public IEnumerable<int> PermissionIds { get; set; }
    }
}
