using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Application.DTOs.Admin;
using WhatBug.Application.Services.Interfaces;
using WhatBug.WebUI.ViewModels.Admin;

namespace WhatBug.WebUI.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly IMapper _mapper;

        public AdminController(IAdminService adminService, IMapper mapper)
        {
            _adminService = adminService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> ProjectRoles()
        {
            var vm = new AllProjectRolesViewModel
            {
                ProjectRoles = _mapper.Map<List<ProjectRoleViewModel>>(await _adminService.GetProjectRolesAsync())
            };
            
            return View(vm);
        }

        [HttpGet]
        public IActionResult CreateProjectRole()
        {
            var vm = new CreateProjectRoleViewModel();
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProjectRole(CreateProjectRoleViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var dto = _mapper.Map<CreateProjectRoleDTO>(vm);
            await _adminService.CreateProjectRole(dto);
            return RedirectToAction(nameof(Index));
        }
    }
}
