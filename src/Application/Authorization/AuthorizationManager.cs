using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Domain.Data;
using WhatBug.Domain.Entities;

namespace WhatBug.Application.Authorization
{
    public class AuthorizationManager : IAuthorizationManager
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IWhatBugDbContext _context;

        private Lazy<Task<HashSet<string>>> _userPermissions;
        private Lazy<Task<Dictionary<int, HashSet<string>>>> _projectPermissions;
        private Dictionary<string, int> _projectKeys = new Dictionary<string, int>();

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
                var projectKey = projectGrouping.First().Project.Key;

                var perms = permissionSchemes
                    .Where(p => p.PermissionSchemeId == projectPermissionSchemeId)
                    .Where(s => roleIds.Contains(s.RoleId))
                    .Select(s => s.Permission.Name)
                    .ToHashSet();

                _projectKeys.Add(projectKey, projectId);
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

        public async Task<bool> HasPermission(string permission, int projectId = default, string issueId = default)
        {
            if (!_currentUserService.IsAuthenticated)
                return false;

            var permissionObj = Permissions.ToEntity(permission);

            if (permissionObj == null)
                return false;

            if (permissionObj.Type == PermissionType.Global)
                return await HasUserPermission(permission);

            if (permissionObj.Type == PermissionType.Project && projectId != default)
                return await HasProjectPermission(permission, projectId);

            if (permissionObj.Type == PermissionType.Project && issueId != default)
                return await HasIssuePermission(permission, issueId);

            return false;
        }

        public async Task<bool> HasAnyPermission(IEnumerable<string> permissions, int projectId = default, string issueId = default)
        {
            foreach (var permission in permissions)
            {
                if (await HasPermission(permission, projectId, issueId))
                    return true;
            }

            return false;
        }

        public async Task<bool> HasAllPermissions(IEnumerable<string> permissions, int projectId = default, string issueId = default)
        {
            foreach (var permission in permissions)
            {
                if (!await HasPermission(permission, projectId, issueId))
                    return false;
            }

            return true;
        }

        private async Task<bool> HasUserPermission(string permission)
        {
            var userPermissions = await _userPermissions.Value;

            return userPermissions.Contains(permission);
        }

        private async Task<bool> HasProjectPermission(string permission, int projectId)
        {
            var projectPermissions = await _projectPermissions.Value;

            if (projectPermissions.TryGetValue(projectId, out var permissions))
                return permissions.Contains(permission);

            return false;
        }

        private async Task<bool> HasIssuePermission(string permission, string issueId)
        {
            await _projectPermissions.Value;

            var projectKey = ExtractProjectKey(issueId);

            if (projectKey != null && _projectKeys.TryGetValue(projectKey, out var projectId))
                return await HasProjectPermission(permission, projectId);

            return false;
        }

        // TODO: This method shouldn't really live here
        private string ExtractProjectKey(string issueId)
        {
            var regex = new Regex(@"([A-Z]+)-[0-9]+");
            var matches = regex.Matches(issueId);

            return matches.Count == 1 && matches[0].Groups.Count == 2 ? matches[0].Groups[1].Value : null;
        }
    }
}