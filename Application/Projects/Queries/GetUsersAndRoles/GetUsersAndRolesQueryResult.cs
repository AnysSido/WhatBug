using System.Collections.Generic;
using WhatBug.Common.Mapping;
using WhatBug.Domain.Entities;

namespace WhatBug.Application.Projects.Queries.GetUsersAndRoles
{
    public class GetUsersAndRolesQueryResult : IMapFrom<Project>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PermissionSchemeName { get; set; }
        public IList<ProjectRoleDTO> Roles { get; set; }
    }

    public class ProjectRoleDTO : IMapFrom<Role>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IList<UserDTO> Users { get; set; }
    }

    public class UserDTO : IMapFrom<User>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
    }
}
