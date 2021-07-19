using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatBug.Application.DTOs.PrioritySchemes;
using WhatBug.Application.Services.Interfaces;
using WhatBug.WebUI.Services.Interfaces;
using WhatBug.WebUI.ViewModels.Priorities;
using WhatBug.WebUI.ViewModels.PrioritySchemes;

namespace WhatBug.WebUI.Controllers
{
    public class PrioritySchemesController : Controller
    {
        private readonly IPrioritySchemeService _prioritySchemeService;
        private readonly IPriorityService _priorityService;
        private readonly IIconService _iconService;
        private readonly IMapper _mapper;

        public PrioritySchemesController(IPrioritySchemeService prioritySchemeService, IPriorityService priorityService, IMapper mapper, IIconService iconService)
        {
            _prioritySchemeService = prioritySchemeService;
            _priorityService = priorityService;
            _mapper = mapper;
            _iconService = iconService;
        }

        public async Task<IActionResult> Index()
        {
            var prioritySchemes = await _prioritySchemeService.GetPrioritySchemesAsync();
            var vm = new PrioritySchemesIndexViewModel()
            {
                PrioritySchemes = _mapper.Map<List<PrioritySchemeViewModel>>(prioritySchemes)
            };

            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var vm = new CreatePrioritySchemeViewModel()
            {
                AllPriorities = _mapper.Map<List<PriorityViewModel>>(await _priorityService.GetPrioritiesAsync())
            };
            
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePrioritySchemeViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.AllPriorities = _mapper.Map<List<PriorityViewModel>>(await _priorityService.GetPrioritiesAsync());
                return View(vm);
            }

            var dto = _mapper.Map<CreatePrioritySchemeDTO>(vm);
            await _prioritySchemeService.CreatePrioritySchemeAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            // TODO: Don't allow default to be edited
            var priorityScheme = await _prioritySchemeService.GetPrioritySchemeAsync(id);
            var vm = new EditPrioritySchemeViewModel()
            {
                Id = priorityScheme.Id,
                Name = priorityScheme.Name,
                Description = priorityScheme.Description,
                SelectedPriorityIds = priorityScheme.Priorities.Select(p => p.Id).ToList(),
                AllPriorities = _mapper.Map<List<PriorityViewModel>>(await _priorityService.GetPrioritiesAsync())
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditPrioritySchemeViewModel vm)
        {
            // TODO: Don't allow default to be edited
            if (!ModelState.IsValid)
            {
                vm.AllPriorities = _mapper.Map<List<PriorityViewModel>>(await _priorityService.GetPrioritiesAsync());
                return View(vm);
            }

            var dto = _mapper.Map<EditPrioritySchemeDTO>(vm);
            await _prioritySchemeService.EditPrioritySchemeAsync(dto);
            return RedirectToAction(nameof(Index));
        }
    }
}
