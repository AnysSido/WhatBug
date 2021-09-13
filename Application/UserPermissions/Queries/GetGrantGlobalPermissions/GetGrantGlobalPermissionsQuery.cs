using MediatR;
using System.Collections.Generic;

namespace WhatBug.Application.UserPermissions.Queries.GetGrantGlobalPermissions
{
    public class GetGrantGlobalPermissionsQuery : IRequest<GrantGlobalPermissionsDTO>
    {
        public int UserId { get; set; }
    }
}
