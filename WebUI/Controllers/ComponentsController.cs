using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhatBug.WebUI.Controllers
{
    public class ComponentsController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> GetCreateIssueComponent()
        {
            return ViewComponent("CreateIssue");
        }
    }
}
