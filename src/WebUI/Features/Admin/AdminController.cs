using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WhatBug.WebUI.Common;

namespace WhatBug.WebUI.Features.Admin
{
    [Route("admin", Name = "Admin")]
    public class AdminController : BaseController
    {
        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}