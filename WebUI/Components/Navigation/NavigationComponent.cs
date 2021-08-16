using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WhatBug.Application.Navigation.Queries.GetDefaultNavigation;
using WhatBug.Application.Navigation.Queries.GetProjectNavigation;
using WhatBug.WebUI.Routing;

namespace WhatBug.WebUI.Components.MainNavigation
{
    [ViewComponent(Name = "Navigation")]
    public class NavigationComponent : ViewComponent
    {
        private readonly IMediator _mediator;

        public NavigationComponent(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (Url.GetRouteCategory() == RouteCategory.None)
            {
                return View("Default", await _mediator.Send(new GetDefaultNavigationQuery()));
            }
            else if (Url.GetRouteCategory() == RouteCategory.Project && Url.TryGetInt("projectId", out int projectId))
            {
                return View("Project", await _mediator.Send(new GetProjectNavigationQuery { ProjectId = projectId }));
            }

            return View("Default", await _mediator.Send(new GetDefaultNavigationQuery()));
        }
    }
}
