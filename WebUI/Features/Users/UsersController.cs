using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WhatBug.Application.Users.Queries.GetUserProfile;
using WhatBug.WebUI.Common;

namespace WhatBug.WebUI.Features.Users
{
    [Route("users", Name = "Users")]
    public class UsersController : BaseController
    {
        [HttpGet("{userId}/profile", Name = "Profile")]
        public async Task<IActionResult> Profile(int userId)
        {
            var result = await Mediator.Send(new GetUserProfileQuery { UserId = userId });
            return View(result.Result);
        }
    }
}