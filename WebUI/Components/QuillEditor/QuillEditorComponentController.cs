using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WhatBug.WebUI.Common;

namespace WhatBug.WebUI.Components.QuillEditor
{
    public class QuillEditorComponentController : BaseController
    {
        public async Task<IActionResult> GetComponent()
        {
            return ViewComponent("QuillEditor");
        }
    }
}
