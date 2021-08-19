using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WhatBug.Application.Accounts.Commands.Register;
using WhatBug.WebUI.Common;
using WhatBug.WebUI.Features.Accounts.Register;

namespace WhatBug.WebUI.Features.Accounts
{
    public class AccountsController : BaseController
    {
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
    }
}
