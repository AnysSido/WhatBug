using System.Collections.Generic;

namespace WhatBug.Application.Admin.Queries.GetRoles
{
    public class GetRolesQueryResult
    {
        public IList<RoleDto> Roles { get; set; }
    }
}