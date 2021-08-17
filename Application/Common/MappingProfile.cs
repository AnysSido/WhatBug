using AutoMapper;
using System.Reflection;
using WhatBug.Common.Mapping;

namespace WhatBug.Application.Common
{
    class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
