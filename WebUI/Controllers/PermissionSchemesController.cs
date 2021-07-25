using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatBug.Application.DTOs.Permissions;
using WhatBug.Application.DTOs.PermissionSchemes;
using WhatBug.Application.Services.Interfaces;
using WhatBug.Domain.Entities;
using WhatBug.WebUI.ViewModels.Admin;
using WhatBug.WebUI.ViewModels.Permissions;
using WhatBug.WebUI.ViewModels.PermissionSchemes;

namespace WhatBug.WebUI.Controllers
{
    public class PermissionSchemesController : Controller
    {
        private readonly IPermissionSchemeService _permissionSchemeService;
        private readonly IPermissionService _permissionService;
        private readonly IAdminService _adminService;
        private readonly IMapper _mapper;

        public PermissionSchemesController(IPermissionSchemeService permissionSchemeService, IMapper mapper, IAdminService adminService, IPermissionService permissionService)
        {
            _permissionSchemeService = permissionSchemeService;
            _mapper = mapper;
            _adminService = adminService;
            _permissionService = permissionService;
        }

        public async Task<IActionResult> Index()
        {
            var vm = new PermissionSchemeIndexViewModel
            {
                PermissionSchemes = _mapper.Map<List<PermissionSchemeViewModel>>(await _permissionSchemeService.GetPermissionSchemesAsync())
            };
            return View(vm);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var vm = new CreatePermissionSchemeViewModel();
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePermissionSchemeViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            };

            var dto = _mapper.Map<CreatePermissionSchemeDTO>(vm);
            await _permissionSchemeService.CreatePermissionSchemeAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("PermissionSchemes/{schemeId}/Permissions")]
        public async Task<IActionResult> Permissions(int schemeId)
        {
            var scheme = await _permissionSchemeService.GetPermissionSchemeAsync(schemeId);
            var vm = new PermissionSchemePermissionsViewModel
            {
                SchemeId = schemeId,
                SchemeName = scheme.Name,
                ProjectRoles = _mapper.Map<List<ProjectRoleViewModel>>(await _adminService.GetProjectRoles())
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> GetProjectRolePermissionsPartial(_ProjectRolePermissionsViewModel vm)
        {            
            var grantedPermissions = _mapper.Map<List<PermissionViewModel>>(
                await _permissionSchemeService.GetProjectRolePermissionsAsync(vm.SchemeId, vm.ProjectRoleId));

            vm.GrantablePermissions = _mapper.Map<List<GrantablePermissionViewModel>>(
                _permissionService.GetAllPermissions(PermissionType.Project));

            foreach (var permission in vm.GrantablePermissions)
                permission.IsGranted = grantedPermissions.Any(p => p.Id == permission.Id);
            
            return PartialView("_ProjectRolePermissionsPartial", vm);
        }

        [HttpPost]
        public async Task<IActionResult> SetProjectRolePermissions(_ProjectRolePermissionsViewModel vm)
        {
            var dto = _mapper.Map<SetProjectRolePermissionsDTO>(vm);
            await _permissionSchemeService.SetProjectRolePermissionsAsync(dto);
            return RedirectToAction(nameof(Permissions), new { schemeId = vm.SchemeId } );
        }
    }
}
