using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatBug.Application.DTOs.Issues;
using WhatBug.Application.DTOs.Permissions;
using WhatBug.Application.DTOs.Projects;
using WhatBug.Application.DTOs.Users;
using WhatBug.Domain.Entities;
using WhatBug.Domain.Entities.Permissions;

namespace WhatBug.Application.Common
{
    class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDTO>();
            CreateMap<User, UserWithPermissionsDTO>()
                .ForMember(
                    dest => dest.User,
                    opt => opt.MapFrom(src => src))
                .ForMember(
                    dest => dest.Permissions,
                    opt => opt.MapFrom(src => src.UserPermissions.Select(p => p.Permission)));

            CreateMap<Permission, PermissionDTO>().ReverseMap();

            CreateMap<ProjectDTO, Project>().ReverseMap();

            CreateMap<IssueDTO, Issue>().ReverseMap();
        }
    }
}
