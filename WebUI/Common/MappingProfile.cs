using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatBug.Application.DTOs.Permissions;
using WhatBug.Application.DTOs.Users;
using WhatBug.WebUI.ViewModels.Permissions;

namespace WhatBug.WebUI.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PermissionDTO, PermissionViewModel>().ReverseMap();
        }
    }
}
