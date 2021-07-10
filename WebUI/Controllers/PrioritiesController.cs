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
using WhatBug.WebUI.ViewModels.Priorities;

namespace WhatBug.WebUI.Controllers
{
    public class PrioritiesController : Controller
    {
        private readonly IPriorityService _priorityService;
        private readonly IPriorityIconService _priorityIconService;
        private readonly IMapper _mapper;

        public PrioritiesController(IPriorityService priorityService, IPriorityIconService priorityIconService, IMapper mapper)
        {
            _priorityService = priorityService;
            _priorityIconService = priorityIconService;
            _mapper = mapper;
        }

        private async Task<List<PriorityIconViewModel>> LoadIconVMs()
        {
            var icons = await _priorityService.LoadIconsAsync();
            return icons.Select(i => new PriorityIconViewModel { ClassName = _priorityIconService.IconNameToClass(i.Name) }).ToList();
        }

        public async Task<IActionResult> Index()
        {
            var priorities = await _priorityService.GetPrioritiesAsync();
            var vm = _mapper.Map<List<PriorityViewModel>>(priorities);
            vm.ForEach(p => p.PriorityIcon.ClassName = _priorityIconService.IconNameToClass(p.PriorityIcon.Name));
            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var vm = new CreatePriorityViewModel()
            {
                Priority = new PriorityViewModel(),
                AllIcons = await LoadIconVMs()
            };
            return View("CreateEdit", vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreatePriorityViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.AllIcons = await LoadIconVMs();
                return View("CreateEdit", vm);
            }

            var dto = new PriorityDTO()
            {
                Name = vm.Priority.Name,
                Description = vm.Priority.Description,
                Color = vm.SelectedIconColor,
                PriorityIcon = new PriorityIconDTO()
                {
                    Name = _priorityIconService.ClassToIconName(vm.SelectedIcon)
                }
            };

            await _priorityService.CreatePriorityAsync(dto);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var dto = await _priorityService.GetPriorityAsync(id);
            var vm = new CreatePriorityViewModel()
            {
                Priority = _mapper.Map<PriorityViewModel>(dto),
                AllIcons = await LoadIconVMs(),
                SelectedIcon = _priorityIconService.IconNameToClass(dto.PriorityIcon.Name),
                SelectedIconColor = dto.Color
            };

            return View("CreateEdit", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CreatePriorityViewModel vm)
        {
            var dto = new PriorityDTO()
            {
                Id = vm.Priority.Id,
                Name = vm.Priority.Name,
                Description = vm.Priority.Description,
                Color = vm.SelectedIconColor,
                PriorityIcon = new PriorityIconDTO()
                {
                    Name = _priorityIconService.ClassToIconName(vm.SelectedIcon)
                }
            };

            await _priorityService.UpdatePriority(dto);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateOrder([FromBody] List<int> ids)
        {
            await _priorityService.UpdatePriorityOrder(ids);
            // TODO: Handle errors
            return Json(new { success = true, text = "Success from controller!" });
        }
    }
}
