using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using WhatBug.Common.Mapping;
using WhatBug.Domain.Entities;

namespace WhatBug.Application.ProjectRoles.Queries.GetDeleteRole
{
    public class GetDeleteRoleConfirmQueryResult : IMapFrom<Role>
    {
        public int RoleId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IList<ProjectDto> ProjectsUsingRole { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Role, GetDeleteRoleConfirmQueryResult>()
                .ForMember(d => d.RoleId, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.ProjectsUsingRole, opt => opt.MapFrom(s => s.ProjectUsers.Select(p => p.Project)));
        }
    }

    public class ProjectDto : IMapFrom<Project>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<UserDto> Users { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Project, ProjectDto>()
                .ForMember(d => d.Users, opt => opt.MapFrom(s => s.RoleUsers.Select(ru => ru.User)));
        }
    }

    public class UserDto : IMapFrom<User>
    {
        public string FirstName { get; set; }
        public string Surname { get; set; }
    }
}