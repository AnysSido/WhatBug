using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WhatBug.Application.Accounts.Commands.Register;
using WhatBug.Application.Common.Interfaces;
using WhatBug.WebUI.Common;
using WhatBug.WebUI.Features.Accounts.Login;
using WhatBug.WebUI.Features.Accounts.Register;

namespace WhatBug.WebUI.Features.Accounts
{
    public class AccountsController : BaseController
    {
        private readonly IAuthenticationProvider _authProvider;

        public AccountsController(IAuthenticationProvider authProvider)
        {
            _authProvider = authProvider;
        }

        [HttpGet]
        public IActionResult Register()
        {
            var vm = new RegisterViewModel();
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            await Mediator.Send(new RegisterCommand { Username = vm.Username, Email = vm.Email, Password = vm.Password });

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Login()
        {
            var vm = new LoginViewModel();
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var result = await _authProvider.SignInAsync(vm.Username, vm.Password, vm.RememberMe);

            if (result)
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("InvalidCredentials", "Incorrect username or password");

            return View(vm);
        }
    }
}
