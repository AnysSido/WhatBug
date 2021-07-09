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
using WhatBug.WebUI.ViewModels.Priorities;

namespace WhatBug.WebUI.Controllers
{
    public class PrioritiesController : Controller
    {
        private readonly IPriorityService _priorityService;
        private readonly IMapper _mapper;
        private readonly Dictionary<string, string> _iconMap;

        private string _iconClassPrefix = "fas fa-fw fa-";

        public PrioritiesController(IPriorityService priorityService, IMapper mapper)
        {
            _priorityService = priorityService;
            _mapper = mapper;

            _iconMap = new Dictionary<string, string>()
            {
                {"ArrowUp", "arrow-up" },
                {"ArrowDown", "arrow-down" },
                {"ArrowLeft", "arrow-left" },
                {"ArrowRight", "arrow-right" },
                {"CircleArrowUp", "arrow-circle-up" },
                {"CircleArrowDown", "arrow-circle-down" },
                {"CircleArrowLeft", "arrow-circle-left" },
                {"CircleArrowRight", "arrow-circle-right" },
                {"ChevronUp", "chevron-up" },
                {"ChevronDown", "chevron-down" },
                {"ChevronLeft", "chevron-left" },
                {"ChevronRight", "chevron-right" },
                {"AngleUp", "angle-up" },
                {"AngleDown", "angle-down" },
                {"AngleLeft", "angle-left" },
                {"AngleRight", "angle-right" },
                {"AnglesUp", "angle-double-up" },
                {"AnglesDown", "angle-double-down" },
                {"AnglesLeft", "angle-double-left" },
                {"AnglesRight", "angle-double-right" },
                {"Exclaimation", "exclamation" },
                {"CircleExclaimation", "exclamation-circle" },
                {"TriangleExclaimation", "exclamation-triangle" },
                {"XMark", "times" },
                {"Ban", "ban" },
                {"Equals", "equals" },
            };
        }

        private string GetIconClassName(string name)
        {
            return _iconMap.ContainsKey(name) ? _iconClassPrefix + _iconMap[name] : _iconClassPrefix + "xmark";
        }

        private string GetIconName(string className)
        {
            var iconClass = className.Substring(_iconClassPrefix.Length);
            return _iconMap.ContainsValue(iconClass) ? _iconMap.FirstOrDefault(x => x.Value == iconClass).Key : string.Empty;
        }

        private async Task<List<PriorityIconViewModel>> LoadIconVMs()
        {
            var icons = await _priorityService.LoadIconsAsync();
            return icons.Select(i => new PriorityIconViewModel { ClassName = GetIconClassName(i.Name) }).ToList();
        }

        public async Task<IActionResult> Index()
        {
            var priorities = await _priorityService.GetPrioritiesAsync();
            var vm = _mapper.Map<List<PriorityViewModel>>(priorities);
            vm.ForEach(p => p.PriorityIcon.ClassName = GetIconClassName(p.PriorityIcon.Name));
            vm = vm.OrderBy(p => p.Order).ToList();
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
                    Name = GetIconName(vm.SelectedIcon)
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
                SelectedIcon = GetIconClassName(dto.PriorityIcon.Name),
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
                    Name = GetIconName(vm.SelectedIcon)
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
