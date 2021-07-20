using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatBug.Application.DTOs.Permissions;
using WhatBug.Application.Services.Interfaces;
using WhatBug.WebUI.ViewModels.PermissionSchemes;

namespace WhatBug.WebUI.Controllers
{
    public class PermissionSchemesController : Controller
    {
        private readonly IPermissionSchemeService _permissionSchemeService;
        private readonly IMapper _mapper;

        public PermissionSchemesController(IPermissionSchemeService permissionSchemeService, IMapper mapper)
        {
            _permissionSchemeService = permissionSchemeService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
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
            await _permissionSchemeService.CreatePermissionScheme(dto);
            return RedirectToAction(nameof(Index));
        }
    }
}
