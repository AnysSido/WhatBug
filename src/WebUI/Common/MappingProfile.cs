using AutoMapper;
using System.Reflection;
using WhatBug.Common.Mapping;

namespace WhatBug.WebUI.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
