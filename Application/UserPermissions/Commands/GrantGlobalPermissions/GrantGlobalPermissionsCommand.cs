using MediatR;
using System.Collections.Generic;

namespace WhatBug.Application.UserPermissions.Commands.GrantGlobalPermissions
{
    public class GrantGlobalPermissionsCommand : IRequest
    {
        public int UserId { get; set; }
        public IEnumerable<int> PermissionIds { get; set; }
    }
}
