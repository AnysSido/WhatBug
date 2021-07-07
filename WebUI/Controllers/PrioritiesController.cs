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
        private readonly Dictionary<string, string> _iconMap;

        private string _iconClassPrefix = "fas fa-fw fa-";

        public PrioritiesController(IPriorityService priorityService)
        {
            _priorityService = priorityService;

            _iconMap = new Dictionary<string, string>()
            {
                {"ArrowUp", "arrow-up" },
                {"ArrowDown", "arrow-down" },
                {"ArrowLeft", "arrow-left" },
                {"ArrowRight", "arrow-right" },
                {"CircleArrowUp", "circle-arrow-up" },
                {"CircleArrowDown", "circle-arrow-down" },
                {"CircleArrowLeft", "circle-arrow-left" },
                {"CircleArrowRight", "circle-arrow-right" },
            };
        }

        private string GetIconClassName(string name)
        {
            return _iconMap.ContainsKey(name) ? _iconClassPrefix + _iconMap[name] : _iconClassPrefix + "arrow-up";
        }

        private string GetIconName(string className)
        {
            var iconClass = className.Substring(_iconClassPrefix.Length);
            return _iconMap.ContainsValue(iconClass) ? _iconMap.FirstOrDefault(x => x.Value == iconClass).Key : string.Empty;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var icons = await _priorityService.LoadIconsAsync();

            var vm = new CreatePriorityViewModel()
            {
                Priority = new PriorityViewModel(),
                AllIcons = icons.Select(i => new PriorityIconViewModel { ClassName = GetIconClassName(i.Name) }).ToList()
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePriorityViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var dto = new PriorityDTO()
            {
                Name = vm.Priority.Name,
                Description = vm.Priority.Description,
                Color = vm.SelectedIconColor,
                Icon = new PriorityIconDTO()
                {
                    Name = GetIconName(vm.SelectedIcon)
                }
            };

            await _priorityService.CreatePriorityAsync(dto);

            return RedirectToAction(nameof(Index));
        }
    }
}
