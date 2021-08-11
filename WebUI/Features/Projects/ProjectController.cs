using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatBug.Application.Projects.Commands.CreateProject;
using WhatBug.Application.Projects.Queries.GetCreateProject;
using WhatBug.Application.Projects.Queries.GetProjects;
using WhatBug.WebUI.Controllers;
using WhatBug.WebUI.Features.Projects.Create;
using WhatBug.WebUI.Features.Projects.Index;

namespace WhatBug.WebUI.Features.Projects
{
    public class ProjectController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var dto = await Mediator.Send(new GetProjectsQuery());
            var vm = Mapper.Map<ProjectsViewModel>(dto);
            return View(vm);
        }

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
            return RedirectToAction(nameof(Index)); // TODO: Remove magic string
        }
    }
}
