using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;

namespace WhatBug.Application.Authorization
{
    public class AuthorizationManager : IAuthorizationManager
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IWhatBugDbContext _context;

        private Lazy<Task<HashSet<string>>> _userPermissions;
        private Lazy<Task<Dictionary<int, HashSet<string>>>> _projectPermissions;

        public AuthorizationManager(ICurrentUserService currentUserService, IWhatBugDbContext context)
        {
            _currentUserService = currentUserService;
            _context = context;

            _userPermissions = new Lazy<Task<HashSet<string>>>(LoadUserPermissions);
            _projectPermissions = new Lazy<Task<Dictionary<int, HashSet<string>>>>(LoadProjectPermissions);
        }

        private async Task<HashSet<string>> LoadUserPermissions()
        {
            if (!_currentUserService.IsAuthenticated)
                return new HashSet<string>();

            var userPermissions = await _context.UserPermissions
            .Where(p => p.UserId == _currentUserService.Id)
            .Select(p => p.Permission.Name)
            .ToListAsync();

            return userPermissions.ToHashSet();
        }

        private async Task<Dictionary<int, HashSet<string>>> LoadProjectPermissions()
        {
            var result = new Dictionary<int, HashSet<string>>();

            if (!_currentUserService.IsAuthenticated)
                return result;

            var userProjectRoles = await _context.ProjectRoleUsers
                .Include(u => u.Project)
                .Where(u => u.UserId == _currentUserService.Id)
                .ToListAsync();

            var permissionSchemes = await _context.PermissionSchemeRolePermissions
                .Include(p => p.Permission)
                .Where(s => userProjectRoles
                    .Select(p => p.Project.PermissionSchemeId).ToHashSet().Contains(s.PermissionSchemeId))
                .ToListAsync();

            foreach (var projectGrouping in userProjectRoles.GroupBy(p => p.ProjectId))
            {
                var projectId = projectGrouping.Key;
                var roleIds = projectGrouping.Select(g => g.RoleId).ToList();
                var projectPermissionSchemeId = projectGrouping.First().Project.PermissionSchemeId;

                var perms = permissionSchemes
                    .Where(p => p.PermissionSchemeId == projectPermissionSchemeId)
                    .Where(s => roleIds.Contains(s.RoleId))
                    .Select(s => s.Permission.Name)
                    .ToHashSet();

                result.Add(projectId, perms);
            }

            return result;
        }

        public async Task<List<int>> GetProjectsWithPermissionAsync(string permission)
        {
            var projectPermissions = await _projectPermissions.Value;

            return projectPermissions
                .Where(p => p.Value.Contains(permission))
                .Select(p => p.Key)
                .ToList();
        }

        public async Task<bool> HasPermission(string permission)
        {
            if (!_currentUserService.IsAuthenticated)
                return false;

            var userPermissions = await _userPermissions.Value;

            return userPermissions.Contains(permission);
        }

        public async Task<bool> HasAnyPermission(IEnumerable<string> permissions, int projectId = default)
        {
            foreach (var permission in permissions)
            {
                if (await HasPermission(permission))
                    return true;
            }

            return false;
        }

        public async Task<bool> HasAllPermissions(IEnumerable<string> permissions, int projectId = default)
        {
            foreach (var permission in permissions)
            {
                if (!await HasPermission(permission))
                    return false;
            }

            return true;
        }
    }
}