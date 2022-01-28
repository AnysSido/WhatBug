using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;

namespace WhatBug.WebUI.Components.TopNavigation
{
    [ViewComponent(Name = "TopNavigation")]
    public class TopNavigationComponent : ViewComponent
    {
        private readonly ICurrentUserService _currentUserService;

        public TopNavigationComponent(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var vm = new TopNavigationComponentViewModel
            {
                UserLoggedIn = _currentUserService.IsAuthenticated,
                UserId = _currentUserService.Id,
                UserEmail = _currentUserService.Email,
                UserFullName = $"{_currentUserService.FirstName} {_currentUserService.Surname}"
            };

            return View(vm);
        }
    }
}
