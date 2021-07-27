using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatBug.WebUI.ViewModels.User;

namespace WhatBug.WebUI.ViewModels.Projects
{
    public class _AddUserToProjectRoleViewModel
    {
        [HiddenInput]
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }

        public IList<UserViewModel> Users { get; set; } = new List<UserViewModel>();

        public ICollection<int> SelectedUserIds { get; set; } = new List<int>();
    }
}