using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WhatBug.WebUI.Features.Home
{
    [Route("", Name = "Home")]
    public class HomeController : Controller
    {
        [HttpGet("", Name = "Home")]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
