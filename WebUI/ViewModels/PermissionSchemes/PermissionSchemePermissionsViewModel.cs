using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatBug.WebUI.ViewModels.Admin;

namespace WhatBug.WebUI.ViewModels.PermissionSchemes
{
    public class PermissionSchemePermissionsViewModel
    {
        [HiddenInput]
        public int SchemeId { get; set; }
        public string SchemeName { get; set; }
        public IList<ProjectRoleViewModel> ProjectRoles { get; set; }
    }
}
