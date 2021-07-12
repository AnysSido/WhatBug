using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatBug.Application.DTOs.Issues;
using WhatBug.Application.DTOs.Permissions;
using WhatBug.Application.DTOs.Priorities;
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
            CreateMap<PermissionDTO, PermissionViewModel>().ReverseMap();
            CreateMap<ProjectDTO, ProjectViewModel>().ReverseMap();
            CreateMap<IssueDTO, IssueViewModel>().ReverseMap();
            CreateMap<CreateIssueDTO, CreateIssueViewModel>().ReverseMap();
            CreateMap<PriorityDTO, PriorityViewModel>().ReverseMap();
            CreateMap<PriorityIconDTO, PriorityIconViewModel>().ReverseMap();
            CreateMap<PrioritySchemeDTO, PrioritySchemeViewModel>().ReverseMap();

            CreateMap<CreatePrioritySchemeViewModel, CreatePrioritySchemeDTO>()
                .ForMember(
                    dest => dest.PriorityIds,
                    opt => opt.MapFrom(src => src.SelectedPriorityIds));
            CreateMap<EditPrioritySchemeViewModel, EditPrioritySchemeDTO>()
                .ForMember(
                    dest => dest.PriorityIds,
                    opt => opt.MapFrom(src => src.SelectedPriorityIds));

            CreateMap<CreateProjectViewModel, CreateProjectDTO>()
                .ForMember(
                    dest => dest.PrioritySchemeId,
                    opt => opt.MapFrom(src => src.SelectedPriorityScheme));
        }
    }
}
