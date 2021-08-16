using AutoMapper;
using System.Reflection;
using WhatBug.Application.DTOs.Common;
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

            // Projects
            CreateMap<ProjectDTO, Project>().ReverseMap();

            // Priorities
            CreateMap<PriorityDTO, Priority>().ReverseMap();

            // Priority Schemes
            CreateMap<PrioritySchemeDTO, PriorityScheme>().ReverseMap();

            // Users
            CreateMap<User, UserDTO>();

            // Common
            CreateMap<Color, ColorDTO>();
            CreateMap<ColorIcon, ColorIconDTO>();
            CreateMap<Icon, IconDTO>();
        }
    }
}
