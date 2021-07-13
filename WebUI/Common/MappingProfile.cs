﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatBug.Application.DTOs.Issues;
using WhatBug.Application.DTOs.Permissions;
using WhatBug.Application.DTOs.Priorities;
using WhatBug.Application.DTOs.PrioritySchemes;
using WhatBug.Application.DTOs.Projects;
using WhatBug.WebUI.ViewModels.Issues;
using WhatBug.WebUI.ViewModels.Permissions;
using WhatBug.WebUI.ViewModels.Priorities;
using WhatBug.WebUI.ViewModels.PrioritySchemes;
using WhatBug.WebUI.ViewModels.Projects;

namespace WhatBug.WebUI.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Projects
            CreateMap<ProjectDTO, ProjectViewModel>().ReverseMap();
            CreateMap<CreateProjectViewModel, CreateProjectDTO>()
                .ForMember(
                    dest => dest.PrioritySchemeId,
                    opt => opt.MapFrom(src => src.SelectedPriorityScheme));

            // Permissions
            CreateMap<PermissionDTO, PermissionViewModel>().ReverseMap();

            // Priorities
            CreateMap<PriorityDTO, PriorityViewModel>().ReverseMap();
            CreateMap<PriorityIconDTO, PriorityIconViewModel>()
                .ForMember(
                    dest => dest.ClassName,
                    opt => opt.MapFrom<PriorityIconClassNameResolver, string>(src => src.Name));

            CreateMap<CreatePriorityViewModel, CreatePriorityDTO>()
                .ForMember(
                    dest => dest.Color,
                    opt => opt.MapFrom(src => src.SelectedIconColor))
                .ForMember(
                    dest => dest.PriorityIconName,
                    opt => opt.MapFrom<PriorityIconNameResolver, string>(src => src.SelectedIcon));

            CreateMap<PriorityDTO, EditPriorityViewModel>()
                .ForMember(
                    dest => dest.SelectedIconColor,
                    opt => opt.MapFrom(src => src.Color))
                .ForMember(
                    dest => dest.SelectedIconName,
                    opt => opt.MapFrom<PriorityIconClassNameResolver, string>(src => src.PriorityIcon.Name));

            CreateMap<EditPriorityViewModel, EditPriorityDTO>()
                .ForMember(
                    dest => dest.Color,
                    opt => opt.MapFrom(src => src.SelectedIconColor))
                .ForMember(
                    dest => dest.PriorityIconName,
                    opt => opt.MapFrom<PriorityIconNameResolver, string>(src => src.SelectedIconName));

            // Priority Schemes
            CreateMap<PrioritySchemeDTO, PrioritySchemeViewModel>().ReverseMap();
            CreateMap<CreatePrioritySchemeViewModel, CreatePrioritySchemeDTO>()
                .ForMember(
                    dest => dest.PriorityIds,
                    opt => opt.MapFrom(src => src.SelectedPriorityIds));
            CreateMap<EditPrioritySchemeViewModel, EditPrioritySchemeDTO>()
                .ForMember(
                    dest => dest.PriorityIds,
                    opt => opt.MapFrom(src => src.SelectedPriorityIds));

            // Issues
            CreateMap<IssueDTO, IssueViewModel>().ReverseMap();
            CreateMap<CreateIssueDTO, CreateIssueViewModel>().ReverseMap();
            CreateMap<IssueDTO, IssueDetailViewModel>();
        }
    }
}
