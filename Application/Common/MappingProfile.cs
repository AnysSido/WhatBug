using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatBug.Application.DTOs.Admin;
using WhatBug.Application.DTOs.Common;
using WhatBug.Application.DTOs.Issues;
using WhatBug.Application.DTOs.Permissions;
using WhatBug.Application.DTOs.PermissionSchemes;
using WhatBug.Application.DTOs.Priorities;
using WhatBug.Application.DTOs.PrioritySchemes;
using WhatBug.Application.DTOs.Projects;
using WhatBug.Application.DTOs.Users;
using WhatBug.Domain.Entities;
using WhatBug.Domain.Entities.Priorities;

namespace WhatBug.Application.Common
{
    class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Admin
            CreateMap<CreateProjectRoleDTO, ProjectRole>();
            CreateMap<ProjectRole, ProjectRoleDTO>();

            // Projects
            CreateMap<ProjectDTO, Project>().ReverseMap();

            // Permissions
            CreateMap<Permission, PermissionDTO>().ReverseMap();
            CreateMap<PermissionScheme, PermissionSchemeDTO>();
            CreateMap<CreatePermissionSchemeDTO, PermissionScheme>();

            // Priorities
            CreateMap<PriorityDTO, Priority>().ReverseMap();
            CreateMap<CreatePriorityDTO, Priority>()
                .ForPath(
                    dest => dest.ColorIcon.ColorId,
                    opt => opt.MapFrom(src => src.ColorId))
                .ForPath(
                    dest => dest.ColorIcon.IconId,
                    opt => opt.MapFrom(src => src.IconId));

            CreateMap<EditPriorityDTO, Priority>()
                .ForPath(
                    dest => dest.ColorIcon.ColorId,
                    opt => opt.MapFrom(src => src.ColorId))
                .ForPath(
                    dest => dest.ColorIcon.IconId,
                    opt => opt.MapFrom(src => src.IconId));

            // Priority Schemes
            CreateMap<PrioritySchemeDTO, PriorityScheme>().ReverseMap();
            CreateMap<CreatePrioritySchemeDTO, PriorityScheme>();
            CreateMap<EditPrioritySchemeDTO, PriorityScheme>();

            // Issues
            CreateMap<IssueDTO, Issue>().ReverseMap();
            CreateMap<CreateIssueDTO, Issue>()
                .AfterMap((src, dest) => dest.AssigneeId = dest.AssigneeId == 0 ? null : dest.AssigneeId);

            CreateMap<Issue, IssueDTO>();
            CreateMap<IssueType, IssueTypeDTO>();

            // Users
            CreateMap<User, UserDTO>();
            CreateMap<User, UserWithPermissionsDTO>()
                .ForMember(
                    dest => dest.User,
                    opt => opt.MapFrom(src => src))
                .ForMember(
                    dest => dest.Permissions,
                    opt => opt.MapFrom(src => src.UserPermissions.Select(p => p.Permission)));

            // Common
            CreateMap<Color, ColorDTO>();
            CreateMap<ColorIcon, ColorIconDTO>();
            CreateMap<Icon, IconDTO>();
        }
    }
}
