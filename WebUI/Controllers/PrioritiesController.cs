using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatBug.Application.DTOs.Priorities;
using WhatBug.Application.Services.Interfaces;
using WhatBug.Domain.Data;
using WhatBug.WebUI.Services.Interfaces;
using WhatBug.WebUI.ViewModels.Common;
using WhatBug.WebUI.ViewModels.Priorities;

namespace WhatBug.WebUI.Controllers
{
    public class PrioritiesController : Controller
    {
        private readonly IPriorityService _priorityService;
        private readonly IColorService _colorService;
        private readonly IMapper _mapper;

        public PrioritiesController(IPriorityService priorityService, IMapper mapper, IColorService colorService)
        {
            _priorityService = priorityService;
            _mapper = mapper;
            _colorService = colorService;
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var vm = _mapper.Map<EditPriorityViewModel>(await _priorityService.GetPriorityAsync(id));
            vm.AllIcons = _mapper.Map<List<IconViewModel>>(await _priorityService.LoadIconsAsync());
            vm.AllColors = _mapper.Map<List<ColorViewModel>>(await _colorService.GetAllAsync());
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditPriorityViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.AllIcons = _mapper.Map<List<IconViewModel>>(await _priorityService.LoadIconsAsync());
                vm.AllColors = _mapper.Map<List<ColorViewModel>>(await _colorService.GetAllAsync());
                return View(vm);
            };
            var dto = _mapper.Map<EditPriorityDTO>(vm);
            await _priorityService.EditPriorityAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateOrder([FromBody] List<int> ids)
        {
            await _priorityService.UpdatePriorityOrderAsync(ids);
            // TODO: Handle errors
            return Json(new { success = true, text = "Success from controller!" });
        }
    }
}
