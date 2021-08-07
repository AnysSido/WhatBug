using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WhatBug.Infrastructure.Identity;
using WhatBug.WebUI.Routing;
using WhatBug.WebUI.ViewModels;

namespace WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<PrincipalUser> _userManager;
        private readonly SignInManager<PrincipalUser> _signInManager;

        public HomeController(ILogger<HomeController> logger, UserManager<PrincipalUser> userManager, SignInManager<PrincipalUser> signInManager)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Index()
        {
            var username = "test5@mytest.com";
            var user = await _userManager.FindByNameAsync(username);
            await _signInManager.SignInAsync(user, isPersistent: true);
            //await AuthenticationHttpContextExtensions.SignOutAsync(HttpContext, "Identity.Application");
            //await _signInManager.SignOutAsync();
            var tmp = User.Claims.Select(c => new { c.Type, c.Value });
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
