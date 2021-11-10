using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WhatBug.Application.Projects.Commands.AssignsUsersToRole;
using WhatBug.Application.Projects.Commands.CreateProject;
using WhatBug.Application.Projects.Queries.GetAssignUsersToRole;
using WhatBug.Application.Projects.Queries.GetCreateProject;
using WhatBug.Application.Projects.Queries.GetProjects;
using WhatBug.Application.Projects.Queries.GetUsersAndRoles;
using WhatBug.WebUI.Common;
using WhatBug.WebUI.Features.Projects.Index;
using WhatBug.WebUI.Routing;

namespace WhatBug.WebUI.Features.Projects
{
    [Route("admin/projects", Name = "Projects")]
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

        [HttpGet("{projectId}/users-and-roles", Name = "UsersAndRoles")]
        [RouteCategory(RouteCategory.Project)]
        public async Task<IActionResult> UsersAndRoles(int projectId)
        {
            var dto = await Mediator.Send(new GetUsersAndRolesQuery { ProjectId = projectId });
            return View(dto);
        }

        [HttpGet("{projectId}/assign-project-roles", Name = "AssignProjectRoles")]
        public async Task<IActionResult> GetAssignUsersToRolePartial(int projectId)
        {
            var dto = await Mediator.Send(new GetAssignUsersToRoleQuery { ProjectId = projectId });
            return PartialView(dto);
        }

        [HttpPost("{projectId}/assign-project-roles", Name = "AssignProjectRoles")]
        public async Task<IActionResult> AssignUsersToRole(AssignUsersToRoleCommand command)
        {
            await Mediator.Send(command);
            return RedirectToAction(nameof(UsersAndRoles), new { projectId = command.ProjectId });
        }
    }
}