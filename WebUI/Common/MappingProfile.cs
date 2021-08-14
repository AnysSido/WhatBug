using AutoMapper;
using System.Reflection;
using WhatBug.Application.DTOs.Common;
using WhatBug.Application.DTOs.Issues;
using WhatBug.Application.DTOs.Priorities;
using WhatBug.Application.DTOs.Projects;
using WhatBug.Application.DTOs.Users;
using WhatBug.WebUI.ViewModels.Common;
using WhatBug.WebUI.ViewModels.Issues;
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

            // User
            CreateMap<UserDTO, UserViewModel>();

            // Projects
            CreateMap<ProjectDTO, ProjectViewModel>();

            // Priorities
            CreateMap<PriorityDTO, PriorityViewModel>();

            // Issues
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
