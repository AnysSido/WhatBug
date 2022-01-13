using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WhatBug.Application.Issues.Commands.CreateIssue;
using WhatBug.Application.Issues.Queries.GetCreateIssue;
using WhatBug.WebUI.Common;

namespace WhatBug.WebUI.Components.CreateIssue
{
    public class CreateIssueComponentController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetComponent(int? projectId = null)
        {
            var result = await Mediator.Send(new GetCreateIssueQuery { ProjectId = projectId });
            var vm = Mapper.Map<CreateIssueViewModel>(result.Result);

            return ViewComponent("CreateIssue", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Refresh(CreateIssueViewModel vm)
        {
            ModelState.Remove(nameof(vm.PriorityId));

            foreach (var modelValue in ModelState.Values)
            {
                modelValue.Errors.Clear();
            }

            return await GetComponent(vm.ProjectId);
        }

        [HttpPost]
        public async Task<IActionResult> CreateIssue(CreateIssueViewModel vm)
        {
            var result = await Mediator.Send(new CreateIssueCommand
            {
                Summary = vm.Summary,
                Description = vm.Description,
                ProjectId = vm.ProjectId,
                IssueTypeId = vm.IssueTypeId,
                PriorityId = vm.PriorityId,
                AssigneeId = vm.AssigneeId,
                ReporterId = vm.ReporterId
            });

            if (result.HasValidationErrors)
            {
                var dto = await Mediator.Send(new GetCreateIssueQuery { ProjectId = vm.ProjectId });
                var viewModel = Mapper.Map<CreateIssueViewModel>(dto.Result);
                return ViewComponentWithErrors(viewModel, result, "CreateIssue");
            }

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
