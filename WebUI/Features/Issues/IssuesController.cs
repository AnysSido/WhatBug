using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WhatBug.Application.Issues.Queries.GetIssueDetail;
using WhatBug.WebUI.Common;
using WhatBug.WebUI.Features.Issues.Detail;

namespace WhatBug.WebUI.Features.Issues
{
    public class IssuesController : BaseController
    {
        private readonly IMapper _mapper;

        public IssuesController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var dto = await Mediator.Send(new GetIssueDetailQuery { Id = id });
            var vm = _mapper.Map<IssueDetailViewModel>(dto);
            return View(vm);
        }
    }
}
