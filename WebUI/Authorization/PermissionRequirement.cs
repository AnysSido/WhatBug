using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhatBug.WebUI.Authorization
{
    /* 
     * AuthorizationHandlers are typically created separately and registered with DI.
     * Since PermissionRequirements are built dynamically based on PermissionAuthorizeAttributes
     * and only ever use the same specific handler, this class acts as both the requirement
     * and the handler and so is able to handle itself.
    */

    public class PermissionRequirement : IAuthorizationRequirement, IAuthorizationHandler
    {
        public static string ClaimType => "Permissions";
        public PermissionOperator PermissionOperator { get; }
        public string[] Permissions { get; }

        public PermissionRequirement(PermissionOperator permissionOperator, string[] permissions)
        {
            if (permissions.Length == 0)
                throw new ArgumentException("At least one permission is required.", nameof(permissions));

            PermissionOperator = permissionOperator;
            Permissions = permissions;
        }

        public async Task HandleAsync(AuthorizationHandlerContext context)
        {
            if (await Handler(context))
            {
                context.Succeed(this);
                return;
            }

            // Another handler for this requirement could be registered with DI and may succeed, thus 
            // bypassing permissions completely. Calling context.Fail() will ensure this requirement
            // fails even if another handler succeeds.
            context.Fail();
        }

        Task<bool> Handler(AuthorizationHandlerContext context)
        {
            // A user must own all of the required permission claims.
            if (PermissionOperator == PermissionOperator.And)
            {
                foreach (var permission in Permissions)
                {
                    if (!context.User.HasClaim(ClaimType, permission))
                    {
                        return Task.FromResult(false);
                    }
                }

                return Task.FromResult(true);
            }

            // A user must own at least one of the required permission claims.
            foreach (var permission in Permissions)
            {
                if (context.User.HasClaim(ClaimType, permission))
                {
                    return Task.FromResult(true);
                }
            }

            return Task.FromResult(false);
        }
    }
}
