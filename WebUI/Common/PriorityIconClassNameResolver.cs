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
    public class PriorityIconClassNameResolver : IMemberValueResolver<object, object, string, string>
    {
        private readonly IPriorityIconService _priorityIconService;

        public PriorityIconClassNameResolver(IPriorityIconService priorityIconService)
        {
            _priorityIconService = priorityIconService;
        }

        public string Resolve(object source, object destination, string sourceMember, string destMember, ResolutionContext context)
        {
            return _priorityIconService.IconNameToClass(sourceMember);
        }
    }

    public class PriorityIconNameResolver : IMemberValueResolver<object, object, string, string>
    {
        private readonly IPriorityIconService _priorityIconService;

        public PriorityIconNameResolver(IPriorityIconService priorityIconService)
        {
            _priorityIconService = priorityIconService;
        }

        public string Resolve(object source, object destination, string sourceMember, string destMember, ResolutionContext context)
        {
            return _priorityIconService.ClassToIconName(sourceMember);
        }
    }
}
