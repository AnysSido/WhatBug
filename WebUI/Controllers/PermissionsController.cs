using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatBug.Application.Services.Interfaces;
using WhatBug.WebUI.ViewModels.Permissions;

namespace WebUI.Controllers
{
    public class PermissionsController : Controller
    {
        private readonly IUserService _userService;
        public PermissionsController(IUserService userService)
        {
            _userService = userService;
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
    }
}
