using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Application.Services.Interfaces;
using WhatBug.WebUI.ViewModels.Projects;

namespace WhatBug.WebUI.ViewComponents
{
    public class MainNavigationViewComponent : ViewComponent
    {
        private readonly IProjectService _projectService;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public MainNavigationViewComponent(IProjectService projectService, ICurrentUserService currentUserService, IMapper mapper)
        {
            _projectService = projectService;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var projects = await _projectService.GetAllProjects();
            var vm = new MainNavigationComponentViewModel
            {
                Projects = _mapper.Map<List<ProjectViewModel>>(projects)
            };

            return View(vm);
        }
    }

    public class MainNavigationComponentViewModel
    {
        public IList<ProjectViewModel> Projects { get; set; } = new List<ProjectViewModel>();
    }
}
