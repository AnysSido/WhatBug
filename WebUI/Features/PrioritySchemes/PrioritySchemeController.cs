using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatBug.Application.PrioritySchemes.Queries.GetPrioritySchemes;
using WhatBug.WebUI.Controllers;
using WhatBug.WebUI.Features.PrioritySchemes.Index;

namespace WhatBug.WebUI.Features.PrioritySchemes
{
    public class PrioritySchemeController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var dto = await Mediator.Send(new GetPrioritySchemesQuery());
            var vm = Mapper.Map<PrioritySchemesViewModel>(dto);
            return View(vm);
        }
    }
}
