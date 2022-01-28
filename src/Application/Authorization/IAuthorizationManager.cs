using System.Collections.Generic;
using System.Threading.Tasks;

namespace WhatBug.Application.Authorization
{
    public interface IAuthorizationManager
    {
        Task<List<int>> GetProjectsWithPermissionAsync(string permission);
        Task<bool> HasAllPermissions(IEnumerable<string> permissions, int projectId = default, string issueId = default);
        Task<bool> HasAnyPermission(IEnumerable<string> permissions, int projectId = default, string issueId = default);
        Task<bool> HasPermission(string permission, int projectId = default, string issueId = default);
    }
}