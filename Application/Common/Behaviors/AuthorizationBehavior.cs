using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Reflection;
using WhatBug.Application.Common.Security;
using WhatBug.Application.Common.Interfaces;
using System;
using Microsoft.EntityFrameworkCore;
using WhatBug.Domain.Entities;
using WhatBug.Application.Common.Exceptions;
using System.Collections.Generic;
using WhatBug.Domain.Data;

namespace WhatBug.Application.Common.Behaviors
{
    public class AuthorizationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IWhatBugDbContext _context;

        public AuthorizationBehavior(ICurrentUserService currentUserService, IWhatBugDbContext context)
        {
            _currentUserService = currentUserService;
            _context = context;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var authorizeAttributes = request.GetType().GetCustomAttributes<AuthorizeAttribute>();

            if (authorizeAttributes.Any())
            {
                if (!_currentUserService.IsAuthenticated)
                    throw new AccessDeniedException();

                var projectId = request is IRequireProjectAuthorization req ? req.ProjectId : default;
                var grantedPermissionIds = await GetGrantedPermissionsIds(projectId);

                if (!grantedPermissionIds.Any())
                    throw new AccessDeniedException();

                var authenticated = false;

                foreach (var authorizeAttribute in authorizeAttributes)
                {
                    var requiredPermissionIds = authorizeAttribute.Permissions.AsEnumerable().Select(p => Permissions.ToEntity(p).Id).ToList();

                    if (authorizeAttribute.Operator == PermissionOperator.And)
                        authenticated = AuthorizeAll(grantedPermissionIds, requiredPermissionIds);
                    else
                        authenticated = AuthorizeAny(grantedPermissionIds, requiredPermissionIds);

                    if (authenticated)
                        break;
                }

                if (!authenticated)
                    throw new AccessDeniedException();
            }

            return await next();
        }

        private bool AuthorizeAll(IEnumerable<int> grantedPermissionIds, IEnumerable<int> requiredPermissionIds)
        {
            foreach (var requiredPermissionId in requiredPermissionIds)
            {
                if (!grantedPermissionIds.Contains(requiredPermissionId))
                    return false;
            }

            return true;
        }

        private bool AuthorizeAny(IEnumerable<int> grantedPermissionIds, IEnumerable<int> requiredPermissionIds)
        {
            foreach (var requiredPermissionId in requiredPermissionIds)
            {
                if (grantedPermissionIds.Contains(requiredPermissionId))
                    return true;
            }

            return false;
        }

        private async Task<IEnumerable<int>> GetGrantedPermissionsIds(int projectId = default)
        {
            IQueryable<User> userQuery = _context.Users.Include(u => u.UserPermissions);

            if (projectId != default)
                userQuery = userQuery.Include(u => u.ProjectRoles);

            var user = await userQuery.FirstOrDefaultAsync(u => u.Id == _currentUserService.Id);
            var grantedPermissionIds = user.UserPermissions.Select(p => p.PermissionId).ToList();

            if (projectId != default)
                grantedPermissionIds.AddRange(await GetGrantedProjectPermissions(user, projectId));

            return grantedPermissionIds;
        }

        private async Task<IEnumerable<int>> GetGrantedProjectPermissions(User user, int projectId)
        {
            var project = await _context.Projects.FindAsync(projectId);

            if (project == null)
            {
                // Throw auth exception
            }

            // Get the roles that the user has for this project
            var usersRoles = user.ProjectRoles.Select(r => r.RoleId);

            // Get the permission scheme for the project
            var permissionScheme = await _context.PermissionSchemes
                .Include(s => s.ProjectRolePermissions)
                .FirstOrDefaultAsync(s => s.Id == project.PermissionSchemeId);

            if (permissionScheme == null)
            {
                // Use default permission scheme
            }

            // Get all the permissions granted to this user's roles on this permission scheme
            var grantedPermissions = permissionScheme.ProjectRolePermissions
                .Where(p => usersRoles.Contains(p.RoleId))
                .Select(p => p.PermissionId);

            return grantedPermissions;
        }
    }
}
