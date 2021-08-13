﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
using WhatBug.Common.Mapping;
using WhatBug.Domain.Entities;
using WhatBug.Domain.Entities.Priorities;

namespace WhatBug.Application.Common
{
    class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());

            // Admin
            CreateMap<CreateProjectRoleDTO, Role>();
            CreateMap<Role, ProjectRoleDTO>();

            // Projects
            CreateMap<ProjectDTO, Project>().ReverseMap();

            // Permissions
            CreateMap<Permission, PermissionDTO>().ReverseMap();
            CreateMap<PermissionScheme, PermissionSchemeDTO>();
            CreateMap<CreatePermissionSchemeDTO, PermissionScheme>();

            // Priorities
            CreateMap<PriorityDTO, Priority>().ReverseMap();

            // Priority Schemes
            CreateMap<PrioritySchemeDTO, PriorityScheme>().ReverseMap();

            // Issues
            CreateMap<CreateIssueDTO, Issue>()
                .AfterMap((src, dest) => dest.AssigneeId = dest.AssigneeId == 0 ? null : dest.AssigneeId);

            CreateMap<IssueType, IssueTypeDTO>();
            CreateMap<IssueStatus, IssueStatusDTO>();

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
