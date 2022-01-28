using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System;
using System.IO;
using System.Linq;

namespace WhatBug.WebUI.ViewLocators
{
    public class FeatureControllerModelConvention : IControllerModelConvention
    {
        private readonly string _folderName;

        public FeatureControllerModelConvention(string folderName)
        {
            _folderName = folderName;
        }

        public void Apply(ControllerModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var featureName = model.ControllerType.Namespace.Split('.')
                                   .SkipWhile(s => s != _folderName)
                                   .Aggregate("", Path.Combine);

            model.Properties.Add("feature", featureName);
        }
    }
}
