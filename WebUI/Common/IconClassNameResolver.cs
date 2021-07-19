using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatBug.Application.DTOs.Priorities;
using WhatBug.WebUI.Services.Interfaces;
using WhatBug.WebUI.ViewModels.Priorities;

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
