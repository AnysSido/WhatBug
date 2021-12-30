using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WhatBug.WebUI.Common;

namespace WhatBug.WebUI.Filters
{
    public class ReadOnlyModeActionFilter : IActionFilter
    {
        /*
         * This filter will add a flag to temp data whenever a POST action is performed.
         * 
         * The flag is read on the next non-POST action in order to populate the ViewData for whichever action is called next.
         * 
         * This is used to display a notification to the user when a POST action was called in read-only mode to inform them
         * that changes will not be persisted in this mode.
         */
        public void OnActionExecuted(ActionExecutedContext context)
        {
            var isPost = context.HttpContext.Request.Method == "POST";

            if (isPost)
            {
                var request = context.HttpContext.Request;

                // Successful POST actions redirect to GET requests. POSTs that do not redirect are typically due to
                // something like failed validation. We don't need to display the notification in those instances.
                if (context.Result is RedirectToActionResult)
                {
                    // Notifications for AJAX requests are handled in javascript by intercepting the AJAX call. We do not need to
                    // do anything here for AJAX requests.
                    if (!Utils.IsAjaxRequest(request) && context.Controller is Controller controller)
                    {
                        controller.TempData["IsPostAction"] = true;
                    }
                }
            }
            else
            {
                if (context.Controller is Controller controller)
                {
                    var isPostAction = controller.TempData["IsPostAction"] != null;

                    if (isPostAction)
                    {
                        var result = context.Result as ViewResult;
                        if (result != null)
                            result.ViewData["IsPostAction"] = "true";
                    }
                }
            }
        }

        public void OnActionExecuting(ActionExecutingContext context) { }
    }
}
