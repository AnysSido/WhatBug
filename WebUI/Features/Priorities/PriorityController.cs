using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatBug.Application.Priorities.Queries.GetPriorities;
using WhatBug.WebUI.Controllers;

namespace WhatBug.WebUI.Features.Priorities
{
    public class PriorityController : BaseController
    {
        public async Task<IActionResult> Index()
        {
            var dto = await Mediator.Send(new GetPrioritiesQuery());
            var vm = Mapper.Map<PrioritiesViewModel>(dto);

            return View(vm);
        }
    }
}
