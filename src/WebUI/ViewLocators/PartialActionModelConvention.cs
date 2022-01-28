using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace WhatBug.WebUI.ViewLocators
{
    public class PartialActionModelConvention : IActionModelConvention
    {
        public void Apply(ActionModel action)
        {
            var isPartialAction = action.ActionName.StartsWith("Get") && action.ActionName.EndsWith("Partial");

            action.Properties.Add("partialdir", isPartialAction ? action.ActionName[3..^7] : string.Empty);
            action.Properties.Add("partialview", isPartialAction ? "_" + action.ActionName[3..^0] : string.Empty);
        }
    }
}
