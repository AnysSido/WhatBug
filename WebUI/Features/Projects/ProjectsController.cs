using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WhatBug.Application.Projects.Commands.AssignsUsersToRole;
using WhatBug.Application.Projects.Commands.CreateProject;
using WhatBug.Application.Projects.Queries.GetAssignUsersToRole;
using WhatBug.Application.Projects.Queries.GetCreateProject;
using WhatBug.Application.Projects.Queries.GetProjects;
using WhatBug.Application.Projects.Queries.GetUsersAndRoles;
using WhatBug.WebUI.Common;
using WhatBug.WebUI.Features.Projects.Create;
using WhatBug.WebUI.Features.Projects.Index;
using WhatBug.WebUI.Routing;

namespace WhatBug.WebUI.Features.Projects
{
    public class ProjectsController : BaseController
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
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [RouteCategory(RouteCategory.Project)]
        public async Task<IActionResult> UsersAndRoles(int projectId)
        {
            var dto = await Mediator.Send(new GetUsersAndRolesQuery { ProjectId = projectId });
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> GetAssignUsersToRolePartial(int projectId)
        {
            var dto = await Mediator.Send(new GetAssignUsersToRoleQuery { ProjectId = projectId });
            return PartialView(dto);
        }

        [HttpPost]
        public async Task<IActionResult> AssignUsersToRole(AssignUsersToRoleCommand command)
        {
            await Mediator.Send(command);
            return RedirectToAction(nameof(UsersAndRoles), new { projectId = command.ProjectId });
        }
    }
}
