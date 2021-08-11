using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatBug.Application.DTOs.Issues;
using WhatBug.Application.Services.Interfaces;
using WhatBug.WebUI.Services.Interfaces;
using WhatBug.WebUI.ViewModels.Issues;
using WhatBug.WebUI.ViewModels.Priorities;

namespace WhatBug.WebUI.Controllers
{
    public class IssuesController : BaseController
    {
        private readonly IIssueService _issueService;
        private readonly IMapper _mapper;

        public IssuesController(IIssueService issueService, IMapper mapper)
        {
            _issueService = issueService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var issue = await _issueService.GetIssue(id);
            var vm = _mapper.Map<IssueDetailViewModel>(issue);
            return View(vm);
        }
    }
}
