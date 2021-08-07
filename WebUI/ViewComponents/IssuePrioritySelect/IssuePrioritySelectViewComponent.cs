using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatBug.Application.Services.Interfaces;
using WhatBug.WebUI.ViewModels.Priorities;

namespace WhatBug.WebUI.ViewComponents
{
    public class IssuePrioritySelectViewComponent : ViewComponent
    {
        private readonly IProjectService _projectsService;
        private readonly IMapper _mapper;

        public IssuePrioritySelectViewComponent(IProjectService projectsService, IMapper mapper)
        {
            _projectsService = projectsService;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(IssuePrioritySelectComponentOptions options)
        {
            var project = await _projectsService.GetProjectAsync(options.projectId);
            var vm = new IssuePrioritySelectComponentViewModel
            {
                AllSchemePriorities = _mapper.Map<List<PriorityViewModel>>(project.PriorityScheme.Priorities)
            };

            ViewData.TemplateInfo.HtmlFieldPrefix = options?.Prefix;
            return View(vm);
        }
    }
}
