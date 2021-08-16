using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WhatBug.WebUI.Features.Home
{
    public class HomeController : Controller
    {
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
