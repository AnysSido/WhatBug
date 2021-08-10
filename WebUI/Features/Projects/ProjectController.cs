using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatBug.Application.Projects.Commands.CreateProject;
using WhatBug.Application.Projects.Queries.GetCreateProject;
using WhatBug.WebUI.Controllers;

namespace WhatBug.WebUI.Features.Projects
{
    public class ProjectController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var dto = await Mediator.Send(new GetCreateProjectQuery());
            var vm = Mapper.Map<CreateViewModel>(dto);
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProjectCommand command)
        {
            await Mediator.Send(command);
            return RedirectToAction("Index", "Projects");
        }
    }
}
