using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WhatBug.Application.Issues.Commands.CreateIssue;
using WhatBug.Application.Issues.Queries.GetCreateIssue;
using WhatBug.WebUI.Controllers;

namespace WhatBug.WebUI.Components.CreateIssue
{
    public class CreateIssueComponentController : BaseController
    {
        public async Task<IActionResult> GetComponent(int? projectId = null)
        {
            var dto = await Mediator.Send(new GetCreateIssueQuery { ProjectId = projectId });
            var vm = Mapper.Map<CreateIssueViewModel>(dto);
            return ViewComponent("CreateIssue", vm);
        }

        public async Task<IActionResult> Refresh(CreateIssueViewModel vm)
        {
            ModelState.Remove(nameof(vm.PriorityId));

            foreach (var modelValue in ModelState.Values)
            {
                modelValue.Errors.Clear();
            }

            return await GetComponent(vm.ProjectId);
        }

        public async Task<IActionResult> CreateIssue(CreateIssueViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return await GetComponent(vm.ProjectId);
            }

            var command = new CreateIssueCommand
            {
                Summary = vm.Summary,
                Description = vm.Description,
                ProjectId = vm.ProjectId,
                IssueTypeId = vm.IssueTypeId,
                PriorityId = vm.PriorityId,
                AssigneeId = vm.AssigneeId,
                ReporterId = vm.ReporterId
            };

            await Mediator.Send(command);

            if (vm.CreateAnother)
            {
                ModelState.Remove(nameof(vm.Summary));
                ModelState.Remove(nameof(vm.Description));
                return await GetComponent();
            }

            return Json(new { success = true });
        }
    }
}
