using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatBug.Application.DTOs.Permissions;
using WhatBug.Application.DTOs.Users;
using WhatBug.Application.Services.Interfaces;
using WhatBug.Domain.Data;
using WhatBug.Domain.Entities.Permissions;
using WhatBug.WebUI.ViewModels.Permissions;

namespace WhatBug.WebUI.Controllers
{
    public class PermissionsController : Controller
    {
        private readonly IPermissionService _permissionService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public PermissionsController(IUserService userService, IMapper mapper, IPermissionService permissionService)
        {
            _userService = userService;
            _mapper = mapper;
            _permissionService = permissionService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("Permissions/Global")]
        public async Task<IActionResult> Global()
        {
            var dtos = await _userService.GetAllUsersWithPermissions();
            return View("~/Views/Permissions/GlobalIndex.cshtml", dtos);
        }

        [HttpGet]
        //[Route("Permissions/Global/Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var allPermissions = _mapper.Map<List<PermissionViewModel>>(_permissionService.GetAllPermissions(PermissionType.Global));
            var userWithPermissions = await _userService.GetUserWithPermissions(id);
            var vm = new PermissionsGlobalEditViewModel(allPermissions, userWithPermissions);

            return View("~/Views/Permissions/GlobalEdit.cshtml", vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PermissionsGlobalEditViewModel vm)
        {
            if (!ModelState.IsValid)
                return View("~/Views/Permissions/GlobalEdit.cshtml", vm);

            var dto = new SetUserPermissionsDTO()
            {
                UserId = vm.UserWithPermissions.User.Id,
                PermissionIds = vm.GrantedPermissionIds
            };

            await _permissionService.SetUserPermissions(dto);

            return RedirectToAction("Global");
        }
    }
}
