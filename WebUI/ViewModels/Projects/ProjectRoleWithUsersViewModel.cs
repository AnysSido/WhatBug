using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatBug.WebUI.ViewModels.User;

namespace WhatBug.WebUI.ViewModels.Projects
{
    public class ProjectRoleWithUsersViewModel
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public IList<UserViewModel> Users = new List<UserViewModel>();
    }
}
