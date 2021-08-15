using AutoMapper;
using System.Reflection;
using WhatBug.Application.DTOs.Projects;
using WhatBug.WebUI.ViewModels.Projects;
using WhatBug.Common.Mapping;

namespace WhatBug.WebUI.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());

            // Projects
            CreateMap<ProjectDTO, ProjectViewModel>();
        }
    }
}
