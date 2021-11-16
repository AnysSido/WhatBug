using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WhatBug.Application.Projects.Commands.CreateProject;
using WhatBug.Application.Projects.Queries.GetCreateProject;
using WhatBug.Application.Projects.Queries.GetProjects;
using WhatBug.WebUI.Common;
using WhatBug.WebUI.Features.Projects.Index;

namespace WhatBug.WebUI.Features.Projects
{
    [Route("projects", Name = "Projects")]
    public class ProjectsController : BaseController
    {
        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var dto = await Mediator.Send(new GetProjectsQuery());
            var vm = Mapper.Map<ProjectsViewModel>(dto);
            return View(vm);
        }

        [HttpGet("create", Name = "CreateProject")]
        public async Task<IActionResult> Create()
        {
            var result = await Mediator.Send(new GetCreateProjectQuery());
            return View(result.Result);
        }

        [HttpPost("create", Name = "CreateProject")]
        public async Task<IActionResult> Create(GetCreateProjectQueryResult vm)
        {
            var result = await Mediator.Send(new CreateProjectCommand
            {
                Name = vm.Name,
                Description = vm.Description,
                Key = vm.Key,
                PrioritySchemeId = vm.PrioritySchemeId,
                PermissionSchemeId = vm.PermissionSchemeId
            });

            if (result.HasValidationErrors)
            {
                var dto = await Mediator.Send(new GetCreateProjectQuery());
                return ViewWithErrors(dto.Result, result);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}