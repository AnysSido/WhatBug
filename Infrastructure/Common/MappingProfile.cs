using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatBug.Application.Services;
using WhatBug.Infrastructure.Identity;

namespace WhatBug.Infrastructure.Common
{
    class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PrincipalUser, UserDTO>()
                .ForMember(dest =>
                    dest.Id,
                    opt => opt.MapFrom(src => src.UserId));
        }
    }
}
