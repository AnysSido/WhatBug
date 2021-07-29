using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatBug.Application.Services.Interfaces;
using WhatBug.WebUI.ViewModels.Projects;

namespace WhatBug.WebUI.ViewComponents
{
    public class DefaultNavigationViewComponent : ViewComponent
    {
        private readonly IProjectService _projectService;
        private readonly IMapper _mapper;

        public DefaultNavigationViewComponent(IProjectService projectService, IMapper mapper)
        {
            _projectService = projectService;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var projects = await _projectService.GetAllProjects();
            var vm = new DefaultNavigationComponentViewModel
            {
                Projects = _mapper.Map<List<ProjectViewModel>>(projects)
            };

            return View(vm);
        }
    }

    public class DefaultNavigationComponentViewModel
    {
        public IList<ProjectViewModel> Projects { get; set; } = new List<ProjectViewModel>();
    }
}
