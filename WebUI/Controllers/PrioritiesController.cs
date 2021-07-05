using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatBug.Application.Services.Interfaces;
using WhatBug.Domain.Data;
using WhatBug.WebUI.ViewModels.Priorities;

namespace WhatBug.WebUI.Controllers
{
    public class PrioritiesController : Controller
    {
        private readonly IPriorityService _priorityService;
        private readonly Dictionary<string, string> _iconMap = new Dictionary<string, string>();

        public PrioritiesController(IPriorityService priorityService)
        {
            _priorityService = priorityService;
        }

        private string GetIconClassName(string name)
        {
            Dictionary<string, string> map = new Dictionary<string, string>()
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

            return map.ContainsKey(name) ? map[name] : "arrow-up";
        }

        public IActionResult Index()
        {
            return View();
        }

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
    }
}
