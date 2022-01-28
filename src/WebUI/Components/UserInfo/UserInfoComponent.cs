using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;

namespace WhatBug.WebUI.Components.UserInfo
{
    [ViewComponent(Name = "UserInfo")]
    public class UserInfoComponent : ViewComponent
    {
        private readonly ICurrentUserService _currentUserService;

        public UserInfoComponent(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var vm = new UserInfoViewModel
            {
                Id = _currentUserService.Id,
                FullName = $"{_currentUserService.FirstName} {_currentUserService.Surname}",
                Email = _currentUserService.Email
            };

            return View(vm);
        }
    }
}
