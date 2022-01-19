using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WhatBug.Application.Home;
using WhatBug.WebUI.Common;

namespace WhatBug.WebUI.Features.Home
{
    [Route("", Name = "Home")]
    public class HomeController : BaseController
    {
        [HttpGet("", Name = "Home")]
        public async Task<IActionResult> Index()
        {
            var dto = await Mediator.Send(new GetHomeQuery());
            return View(dto.Result);
        }
    }
}
