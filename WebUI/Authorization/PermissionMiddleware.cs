using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WhatBug.Application.Services.Interfaces;

namespace WhatBug.WebUI.Authorization
{
    /*
     * This middleware sits between authentication and authorization and loads
     * the current users' permissions from the db. These permissions are converted
     * to claims and attached to a new ClaimsIdentity, which is then attached to the
     * current ClaimsPrincipal (current user) to be used for authorization.
     * 
     * For a high number of users this functionality may be better performed on startup.
     */
    public class PermissionMiddleware
    {
        private readonly RequestDelegate _next;

        public PermissionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IGlobalPermissionService globalPermissionService)
        {
            if (context.User.Identity == null || !context.User.Identity.IsAuthenticated)
            {
                await _next(context);
                return;
            }

            var userId = int.TryParse(context.User?.FindFirstValue("UserId"), out var id) ? id : -1;
            if (userId == -1)
            {
                // TODO: Handle this. Something went terribly wrong.
                return;
            }

            // Load the users' permissions and convert them to claims
            var userPermissions = await globalPermissionService.GetUserGlobalPermissionsAsync(userId);
            var userPermissionClaims = userPermissions.Select(p => new Claim("Permissions", p.Name));

            // Build a new identity and add the claims, then attach the identity to the user principal.
            var permissionsIdentity = new ClaimsIdentity(nameof(PermissionMiddleware), "name", "role");
            permissionsIdentity.AddClaims(userPermissionClaims);
            context.User.AddIdentity(permissionsIdentity);

            await _next(context);
        }

    }
}
