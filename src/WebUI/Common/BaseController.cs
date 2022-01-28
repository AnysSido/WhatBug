using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using WhatBug.Application.Common.MediatR;

namespace WhatBug.WebUI.Common
{
    public abstract class BaseController : Controller
    {
        private IMediator _mediator;
        private IMapper _mapper;

        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
        protected IMapper Mapper => _mapper ??= HttpContext.RequestServices.GetService<IMapper>();

        public ViewResult ViewWithErrors(object model, Response result)
        {
            foreach (var error in result.ValidationErrors)
                ModelState.AddModelError(error.PropertyName, error.Message);

            return View(model);
        }

        public ViewComponentResult ViewComponentWithErrors(object model, Response result, string componentNane)
        {
            foreach (var error in result.ValidationErrors)
                ModelState.AddModelError(error.PropertyName, error.Message);

            return ViewComponent(componentNane, model);
        }
    }
}
