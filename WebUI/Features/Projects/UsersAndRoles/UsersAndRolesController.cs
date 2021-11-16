using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WhatBug.Application.Projects.Commands.AssignsUsersToRole;
using WhatBug.Application.Projects.Commands.UnassignUsersFromRole;
using WhatBug.Application.Projects.Queries.GetAssignUsersToRole;
using WhatBug.Application.Projects.Queries.GetUsersAndRoles;
using WhatBug.WebUI.Common;
using WhatBug.WebUI.Routing;

namespace WhatBug.WebUI.Features.Projects.UsersAndRoles
{
    [Route("projects/{projectId}/users-and-roles", Name = "UsersAndRoles")]
    public class UsersAndRolesController : BaseController
    {
        [HttpGet("")]
        [RouteCategory(RouteCategory.Project)]
        public async Task<IActionResult> Index(int projectId)
        {
            var result = await Mediator.Send(new GetUsersAndRolesQuery { ProjectId = projectId });
            return View(result.Result);
        }

        [HttpGet("{roleId}/assign", Name = "AssignProjectRoles")]
        [RouteCategory(RouteCategory.Project)]
        public async Task<IActionResult> AssignUsersToRole(int projectId, int roleId)
        {
            var result = await Mediator.Send(new GetAssignUsersToRoleQuery { ProjectId = projectId, RoleId = roleId });
            return View(result.Result);
        }

        [HttpPost("{roleId}/assign", Name = "AssignProjectRoles")]
        public async Task<IActionResult> AssignUsersToRole(AssignUsersToRoleCommand command)
        {
            await Mediator.Send(command);
            return RedirectToAction(nameof(AssignUsersToRole), new { projectId = command.ProjectId, roleId = command.RoleId });
        }

        [HttpPost("unassign", Name = "UnassignProjectRoles")]
        public async Task<IActionResult> UnassignUsersFromRole(UnassignUsersFromRoleCommand command)
        {
            await Mediator.Send(command);
            return RedirectToAction(nameof(AssignUsersToRole), new { projectId = command.ProjectId, roleId = command.RoleId });
        }
    }
}
