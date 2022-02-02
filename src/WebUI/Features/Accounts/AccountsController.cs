using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using WhatBug.Application.Accounts.Commands.Register;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Application.Common.Settings;
using WhatBug.WebUI.Common;
using WhatBug.WebUI.Features.Accounts.Login;
using WhatBug.WebUI.Features.Accounts.Register;

namespace WhatBug.WebUI.Features.Accounts
{
    [AllowAnonymous]
    public class AccountsController : BaseController
    {
        private readonly IAuthenticationProvider _authProvider;
        private readonly WhatBugSettings _whatBugSettings;

        public AccountsController(IAuthenticationProvider authProvider, IOptions<WhatBugSettings> whatbugSettings)
        {
            _authProvider = authProvider;
            _whatBugSettings = whatbugSettings.Value;
        }

        [HttpGet("register", Name = "Register")]
        public IActionResult Register()
        {
            if (!_whatBugSettings.Accounts.RegistrationEnabled)
                return RedirectToAction(nameof(Login));

            return View();
        }

        [HttpPost("register", Name = "Register")]
        public async Task<IActionResult> Register(RegisterViewModel vm)
        {
            if (!_whatBugSettings.Accounts.RegistrationEnabled)
                return RedirectToAction(nameof(Login));

            if (!ModelState.IsValid)
                return View(vm);
            
            await Mediator.Send(new RegisterCommand { Username = vm.Username, Email = vm.Email, Password = vm.Password });

            return RedirectToAction("Index", "Home");
        }

        [HttpGet("login", Name = "Login")]
        public IActionResult Login()
        {
            var vm = new LoginViewModel 
            { 
                RegistrationEnabled = _whatBugSettings.Accounts.RegistrationEnabled,
                DemoEnabled = _whatBugSettings.Accounts.DemoEnabled
            };
            return View(vm);
        }

        [HttpPost("login", Name = "Login")]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var result = await _authProvider.SignInAsync(vm.Username, vm.Password, vm.RememberMe);

            if (result)
                return RedirectToAction("Index", "Home");

            ModelState.AddModelError("InvalidCredentials", "Incorrect username or password");

            return View(vm);
        }

        [HttpPost("login-demo", Name = "LoginDemo")]
        public async Task<IActionResult> LoginDemo()
        {
            await _authProvider.SignInDemoAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet("logout", Name = "Logout")]
        public async Task<IActionResult> Logout()
        {
            await _authProvider.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
    }
}
