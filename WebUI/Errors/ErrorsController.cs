using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using WhatBug.Application.Common.Exceptions;

namespace WhatBug.WebUI.Errors
{
    [AllowAnonymous]
    public class ErrorsController : Controller
    {
        [Route("error")]
        public IActionResult Error()
        {
            var error = HttpContext.Features.Get<IExceptionHandlerPathFeature>()?.Error;

            string code;
            string title;

            switch (error)
            {
                case AccessDeniedException:
                    code = "403";
                    title = ReasonPhrases.GetReasonPhrase(403);
                    break;
                case RecordNotFoundException:
                    code = "404";
                    title = ReasonPhrases.GetReasonPhrase(404);
                    break;
                default:
                    code = "500";
                    title = "Oops, something went wrong!";
                    break;
            }

            return View("/Errors/Error.cshtml", new ErrorViewModel { Code = code, Title = title });
        }

        [Route("error/{code}")]
        public IActionResult Error(int code)
        {
            var reasonPhrase = ReasonPhrases.GetReasonPhrase(code);

            return View("/Errors/Error.cshtml", new ErrorViewModel { Code = code.ToString(), Title = reasonPhrase });
        }
    }
}