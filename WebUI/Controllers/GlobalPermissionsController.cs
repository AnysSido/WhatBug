using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatBug.Application.DTOs.GlobalPermissions;
using WhatBug.Application.Services.Interfaces;
using WhatBug.Domain.Entities;
using WhatBug.WebUI.ViewModels.GlobalPermissions;
using WhatBug.WebUI.ViewModels.Permissions;
using WhatBug.WebUI.ViewModels.User;

namespace WhatBug.WebUI.Controllers
{
    public class GlobalPermissionsController : Controller
    {
        private readonly IGlobalPermissionService _globalPermissionService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public GlobalPermissionsController(IUserService userService, IMapper mapper, IGlobalPermissionService globalPermissionService)
        {
            _userService = userService;
            _mapper = mapper;
            _globalPermissionService = globalPermissionService;
        }

        public async Task<IActionResult> Index()
        {
            var vm = new GlobalPermissionIndexViewModel
            {
                Users = _mapper.Map<List<UserWithPermissionsViewModel>>(await _userService.GetAllUsersWithPermissions())
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> GetGlobalPermissionsPartial(_SetGlobalPermissionsViewModel vm)
        {
            var grantedPermissions = _mapper.Map<List<PermissionViewModel>>(
                await _globalPermissionService.GetUserGlobalPermissionsAsync(vm.UserId));

            vm.GrantablePermissions = _mapper.Map<List<GrantablePermissionViewModel>>(
                _globalPermissionService.GetAvailableGlobalPermissions());

            foreach (var permission in vm.GrantablePermissions)
                permission.IsGranted = grantedPermissions.Any(p => p.Id == permission.Id);

            return PartialView("_SetGlobalPermissionsPartial", vm);
        }

        [HttpPost]
        public async Task<IActionResult> SetGlobalPermissions(_SetGlobalPermissionsViewModel vm)
        {
            var dto = _mapper.Map<SetGlobalPermissionsDTO>(vm);
            await _globalPermissionService.SetGlobalPermissionsAsync(dto);
            return RedirectToAction(nameof(Index));
        }
    }
}
