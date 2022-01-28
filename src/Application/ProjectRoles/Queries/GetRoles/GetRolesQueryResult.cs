using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using WhatBug.Common.Mapping;
using WhatBug.Domain.Entities;

namespace WhatBug.Application.ProjectRoles.Queries.GetRoles
{
    public class GetRolesQueryResult
    {
        public IList<RoleDto> Roles { get; set; }
    }

    public class RoleDto : IMapFrom<Role>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IList<ProjectDto> Projects { get; set; }
    }

    public class ProjectDto : IMapFrom<Project>
    {
        public string Name { get; set; }
        public IList<UserDto> Users { get; set; }
    }

    public class UserDto : IMapFrom<User>
    {
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
    }
}