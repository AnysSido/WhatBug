using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatBug.Application.DTOs.Issues;
using WhatBug.Application.DTOs.Permissions;
using WhatBug.Application.DTOs.Priorities;
using WhatBug.Application.DTOs.Projects;
using WhatBug.Application.DTOs.Users;
using WhatBug.Domain.Entities;
using WhatBug.Domain.Entities.Permissions;
using WhatBug.Domain.Entities.Priorities;

namespace WhatBug.Application.Common
{
    class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Projects
            CreateMap<ProjectDTO, Project>().ReverseMap();

            // Permissions
            CreateMap<Permission, PermissionDTO>().ReverseMap();

            // Priorities
            CreateMap<PriorityDTO, Priority>().ReverseMap();
            CreateMap<PriorityIconDTO, PriorityIcon>().ReverseMap();
            CreateMap<CreatePriorityDTO, Priority>();
            CreateMap<EditPriorityDTO, Priority>();

            // Priority Schemes
            CreateMap<PrioritySchemeDTO, PriorityScheme>().ReverseMap();
            CreateMap<CreatePrioritySchemeDTO, PriorityScheme>();
            CreateMap<EditPrioritySchemeDTO, PriorityScheme>();

            // Issues
            CreateMap<IssueDTO, Issue>().ReverseMap();
            CreateMap<CreateIssueDTO, Issue>()
                .AfterMap((src, dest) => dest.AssigneeId = dest.AssigneeId == 0 ? null : dest.AssigneeId);

            // Users
            CreateMap<User, UserDTO>();
            CreateMap<User, UserWithPermissionsDTO>()
                .ForMember(
                    dest => dest.User,
                    opt => opt.MapFrom(src => src))
                .ForMember(
                    dest => dest.Permissions,
                    opt => opt.MapFrom(src => src.UserPermissions.Select(p => p.Permission)));
        }
    }
}
