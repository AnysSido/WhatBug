using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using WhatBug.Common.Mapping;
using WhatBug.Domain.Entities;

namespace WhatBug.Application.Projects.Queries.GetUsersAndRoles
{
    public class ProjectDTO : IMapFrom<Project>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<ProjectRoleDTO> Roles { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Project, ProjectDTO>()
                .ForMember(d => d.Roles, opt => opt.MapFrom(s => s.RoleUsers.Select(p => p.Role)));
        }
    }
}
