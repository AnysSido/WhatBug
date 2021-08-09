using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using WhatBug.Application.Common;
using WhatBug.Application.DTOs.Admin;
using WhatBug.Application.DTOs.Common;
using WhatBug.Application.DTOs.GlobalPermissions;
using WhatBug.Application.DTOs.Issues;
using WhatBug.Application.DTOs.Permissions;
using WhatBug.Application.DTOs.PermissionSchemes;
using WhatBug.Application.DTOs.Priorities;
using WhatBug.Application.DTOs.PrioritySchemes;
using WhatBug.Application.DTOs.Projects;
using WhatBug.Application.DTOs.Users;
using WhatBug.WebUI.ViewModels.Admin;
using WhatBug.WebUI.ViewModels.Common;
using WhatBug.WebUI.ViewModels.GlobalPermissions;
using WhatBug.WebUI.ViewModels.Issues;
using WhatBug.WebUI.ViewModels.Permissions;
using WhatBug.WebUI.ViewModels.PermissionSchemes;
using WhatBug.WebUI.ViewModels.Priorities;
using WhatBug.WebUI.ViewModels.PrioritySchemes;
using WhatBug.WebUI.ViewModels.Projects;
using WhatBug.WebUI.ViewModels.User;
using WhatBug.Common.Mapping;

namespace WhatBug.WebUI.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());

            // Admin
            CreateMap<CreateProjectRoleViewModel, CreateProjectRoleDTO>();
            CreateMap<ProjectRoleDTO, ProjectRoleViewModel>();

            // User
            CreateMap<UserDTO, UserViewModel>();
            CreateMap<UserWithPermissionsDTO, UserWithPermissionsViewModel>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => src.User.Id))
                .ForMember(
                    dest => dest.Username,
                    opt => opt.MapFrom(src => src.User.Username));

            // Projects
            CreateMap<ProjectDTO, ProjectViewModel>();
            CreateMap<CreateProjectViewModel, CreateProjectDTO>()
                .ForMember(
                    dest => dest.PrioritySchemeId,
                    opt => opt.MapFrom(src => src.SelectedPriorityScheme));
            CreateMap<_AddUserToProjectRoleViewModel, AddUsersToProjectRoleDTO>()
                .ForMember(
                    dest => dest.ProjectRoleId,
                    opt => opt.MapFrom(src => src.SelectedRoleId))
                .ForMember(
                    dest => dest.UserIds,
                    opt => opt.MapFrom(u => u.SelectedUserIds));
            CreateMap<ProjectRoleWithUsersDTO, ProjectRoleWithUsersViewModel>();

            // Permissions
            CreateMap<PermissionDTO, PermissionViewModel>().ReverseMap();
            CreateMap<PermissionDTO, GrantablePermissionViewModel>();

            // Global Permissions
            CreateMap<_SetGlobalPermissionsViewModel, SetGlobalPermissionsDTO>();

            // Permission Schemes
            CreateMap<CreatePermissionSchemeViewModel, CreatePermissionSchemeDTO>();
            CreateMap<PermissionSchemeDTO, PermissionSchemeViewModel>();
            CreateMap<_SetProjectRolePermissionsViewModel, SetProjectRolePermissionsDTO>();

            // Priorities
            CreateMap<PriorityDTO, PriorityViewModel>();

            CreateMap<PriorityDTO, EditPriorityViewModel>()
                .ForMember(
                    dest => dest.SelectedColor,
                    opt => opt.MapFrom(src => src.ColorIcon.Color.Id))
                .ForMember(
                    dest => dest.SelectedIcon,
                    opt => opt.MapFrom(src => src.ColorIcon.Icon.Id));

            CreateMap<CreatePriorityViewModel, CreatePriorityDTO>()
                .ForMember(
                    dest => dest.ColorId,
                    opt => opt.MapFrom(src => src.SelectedColor))
                .ForMember(
                    dest => dest.IconId,
                    opt => opt.MapFrom(src => src.SelectedIcon));

            CreateMap<EditPriorityViewModel, EditPriorityDTO>()
                .ForMember(
                    dest => dest.ColorId,
                    opt => opt.MapFrom(src => src.SelectedColor))
                .ForMember(
                    dest => dest.IconId,
                    opt => opt.MapFrom(src => src.SelectedIcon));

            // Priority Schemes
            CreateMap<PrioritySchemeDTO, PrioritySchemeViewModel>();
            CreateMap<CreatePrioritySchemeViewModel, CreatePrioritySchemeDTO>()
                .ForMember(
                    dest => dest.PriorityIds,
                    opt => opt.MapFrom(src => src.SelectedPriorityIds));
            CreateMap<EditPrioritySchemeViewModel, EditPrioritySchemeDTO>()
                .ForMember(
                    dest => dest.PriorityIds,
                    opt => opt.MapFrom(src => src.SelectedPriorityIds));

            // Issues
            CreateMap<IssueDTO, IssueViewModel>();
            CreateMap<IssueStatusDTO, IssueStatusViewModel>();
            CreateMap<CreateIssueDTO, CreateIssueViewModel>();

            CreateMap<CreateIssueViewModel, CreateIssueDTO>()
                .ForMember(
                    dest => dest.PriorityId,
                    opt => opt.MapFrom(src => src.SelectedPriorityId))
                .ForMember(
                    dest => dest.IssueTypeId,
                    opt => opt.MapFrom(src => src.SelectedIssueType));

            CreateMap<IssueDTO, IssueDetailViewModel>();
            CreateMap<IssueTypeDTO, IssueTypeViewModel>();

            // Common
            CreateMap<ColorDTO, ColorViewModel>().ReverseMap();
            CreateMap<ColorIconDTO, ColorIconViewModel>().ReverseMap();
            CreateMap<IconDTO, IconViewModel>()
                .ForMember(
                    dest => dest.ClassName,
                    opt => opt.MapFrom<IconClassNameResolver, string>(src => src.Name));
        }
    }
}
