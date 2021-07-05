using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatBug.WebUI.ViewModels.Priorities;

namespace WhatBug.WebUI.Controllers
{
    public class PrioritiesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Create()
        {
            var vm = new PriorityViewModel()
            {

            };

            return View(vm);
        }
    }
}
