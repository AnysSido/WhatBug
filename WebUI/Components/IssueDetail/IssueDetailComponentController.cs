using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Application.Issues.Commands.AddComment;
using WhatBug.Application.Issues.Commands.SetIssueDescription;
using WhatBug.Application.Issues.Queries.GetComments;
using WhatBug.Application.Issues.Queries.GetIssueDetail;
using WhatBug.WebUI.Common;

namespace WhatBug.WebUI.Components.IssueDetail
{
    public class IssueDetailComponentController : BaseController
    {
        private readonly ICurrentUserService _currentUserService;

        public IssueDetailComponentController(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }

        public async Task<IActionResult> GetComponent(string issueId)
        {
            var dto = await Mediator.Send(new GetIssueDetailQuery { IssueId = issueId });
            var vm = Mapper.Map<IssueDetailViewModel>(dto);
            return ViewComponent("IssueDetail", vm);
        }

        public async Task<IActionResult> UpdateDescription(string issueId, string description)
        {
            await Mediator.Send(new SetIssueDescriptionCommand { IssueId = issueId, Description = description });
            return Json(new { success = true });
        }

        public async Task<IActionResult> AddComment(string issueId, string comment)
        {
            await Mediator.Send(new AddCommentCommand { IssueId = issueId, Content = comment, AuthorId = _currentUserService.Id });
            return Json(new { success = true });
        }

        public async Task<IActionResult> GetCommentsPartial(string issueId)
        {
            var result = await Mediator.Send(new GetCommentsQuery { IssueId = issueId });

            return PartialView("/Components/IssueDetail/_Comments.cshtml", result.Result);
        }
    }
}
