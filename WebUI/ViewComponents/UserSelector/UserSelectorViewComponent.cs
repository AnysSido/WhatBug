using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatBug.Application.Services.Interfaces;
using WhatBug.WebUI.ViewModels.User;

namespace WhatBug.WebUI.ViewComponents
{
    public class UserSelectorViewComponent : ViewComponent
    {
        private readonly IUserService _userService;
        private readonly IProjectService _projectService;
        private readonly IMapper _mapper;

        public UserSelectorViewComponent(IUserService userService, IMapper mapper, IProjectService projectService)
        {
            _userService = userService;
            _mapper = mapper;
            _projectService = projectService;
        }

        public virtual async Task<List<UserViewModel>> GetUsers(int? projectId)
        {
            if (projectId != null)
                return _mapper.Map<List<UserViewModel>>(await _projectService.GetProjectUsersAsync(projectId ?? default(int)));            

            return _mapper.Map<List<UserViewModel>>(await _userService.GetAllUsersAsync());
        }

        public async Task<IViewComponentResult> InvokeAsync(UserSelectorComponentOptions options)
        {
            var vm = new UserSelectorComponentViewModel
            {
                Users = await GetUsers(options.projectId)
            };

            ViewData.TemplateInfo.HtmlFieldPrefix = options?.Prefix;

            return View(vm);
        }
    }
}
