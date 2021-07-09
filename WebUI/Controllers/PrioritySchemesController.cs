using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatBug.Application.DTOs.Priorities;
using WhatBug.Application.Services.Interfaces;
using WhatBug.WebUI.ViewModels.Priorities;
using WhatBug.WebUI.ViewModels.PrioritySchemes;

namespace WhatBug.WebUI.Controllers
{
    public class PrioritySchemesController : Controller
    {
        private readonly IPrioritySchemeService _prioritySchemeService;
        private readonly IPriorityService _priorityService;
        private readonly IMapper _mapper;

        public PrioritySchemesController(IPrioritySchemeService prioritySchemeService, IPriorityService priorityService, IMapper mapper)
        {
            _prioritySchemeService = prioritySchemeService;
            _priorityService = priorityService;
            _mapper = mapper;
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
            var vm = new CreatePrioritySchemeViewModel();
            vm.SetSelectList(_mapper.Map<List<PriorityViewModel>>(await _priorityService.GetPrioritiesAsync()));
            
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePrioritySchemeViewModel vm)
        {
            var dto = _mapper.Map<CreatePrioritySchemeDTO>(vm);
            await _prioritySchemeService.CreatePrioritySchemeAsync(dto);
            return RedirectToAction(nameof(Index));
        }
    }
}
