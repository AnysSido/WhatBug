using AutoMapper;
using System.Reflection;
using WhatBug.Application.DTOs.Admin;
using WhatBug.Application.DTOs.Common;
using WhatBug.Application.DTOs.Issues;
using WhatBug.Application.DTOs.Permissions;
using WhatBug.Application.DTOs.PermissionSchemes;
using WhatBug.Application.DTOs.Priorities;
using WhatBug.Application.DTOs.Projects;
using WhatBug.Application.DTOs.Users;
using WhatBug.WebUI.ViewModels.Admin;
using WhatBug.WebUI.ViewModels.Common;
using WhatBug.WebUI.ViewModels.Issues;
using WhatBug.WebUI.ViewModels.Permissions;
using WhatBug.WebUI.ViewModels.PermissionSchemes;
using WhatBug.WebUI.ViewModels.Priorities;
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
            CreateMap<ProjectRoleDTO, ProjectRoleViewModel>();

            // User
            CreateMap<UserDTO, UserViewModel>();

            // Projects
            CreateMap<ProjectDTO, ProjectViewModel>();

            // Permissions
            CreateMap<PermissionDTO, PermissionViewModel>().ReverseMap();
            CreateMap<PermissionDTO, GrantablePermissionViewModel>();

            // Permission Schemes
            CreateMap<_SetProjectRolePermissionsViewModel, SetProjectRolePermissionsDTO>();

            // Priorities
            CreateMap<PriorityDTO, PriorityViewModel>();

            // Issues
            CreateMap<IssueStatusDTO, IssueStatusViewModel>();
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
