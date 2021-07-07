using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatBug.Application.DTOs.Issues;
using WhatBug.Application.DTOs.Permissions;
using WhatBug.Application.DTOs.Priorities;
using WhatBug.Application.DTOs.Projects;
using WhatBug.WebUI.ViewModels.Issue;
using WhatBug.WebUI.ViewModels.Permissions;
using WhatBug.WebUI.ViewModels.Priorities;
using WhatBug.WebUI.ViewModels.Project;

namespace WhatBug.WebUI.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PermissionDTO, PermissionViewModel>().ReverseMap();
            CreateMap<ProjectDTO, ProjectViewModel>().ReverseMap();
            CreateMap<IssueDTO, IssueViewModel>().ReverseMap();
            CreateMap<PriorityDTO, PriorityViewModel>().ReverseMap();
            CreateMap<PriorityIconDTO, PriorityIconViewModel>().ReverseMap();
        }
    }
}
