using Microsoft.AspNetCore.Http;

namespace WhatBug.WebUI.Common
{
    public static class Utils
    {
        public static bool IsAjaxRequest(HttpRequest request)
        {
            if (request == null)
                return false;

            if (request.Headers == null)
                return false;

            if (request.Headers["X-Requested-With"] != "XMLHttpRequest")
                return false;

            return true;
        }
    }
}
