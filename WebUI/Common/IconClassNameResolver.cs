using AutoMapper;
using WhatBug.WebUI.Services.Interfaces;

namespace WhatBug.WebUI.Common
{
    public class IconClassNameResolver : IMemberValueResolver<object, object, string, string>
    {
        private readonly IIconService _iconService;

        public IconClassNameResolver(IIconService iconService)
        {
            _iconService = iconService;
        }

        public string Resolve(object source, object destination, string sourceMember, string destMember, ResolutionContext context)
        {
            return _iconService.IconNameToClass(sourceMember);
        }
    }
}
