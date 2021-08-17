using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using WhatBug.Common.Mapping;
using WhatBug.Domain.Entities;

namespace WhatBug.Application.Projects.Queries.GetUsersAndRoles
{
    public class ProjectRoleDTO : IMapFrom<Role>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IList<UserDTO> Users { get; set; } 

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Role, ProjectRoleDTO>()
                .ForMember(d => d.Users, opt => opt.MapFrom(s => s.ProjectUsers.Select(u => u.User)));
        }
    }
}
