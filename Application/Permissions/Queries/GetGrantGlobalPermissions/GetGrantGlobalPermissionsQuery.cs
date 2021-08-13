using MediatR;
using System.Collections.Generic;

namespace WhatBug.Application.Permissions.Queries.GetGrantGlobalPermissions
{
    public class GetGrantGlobalPermissionsQuery : IRequest<GrantGlobalPermissionsDTO>
    {
        public int UserId { get; set; }
    }
}
