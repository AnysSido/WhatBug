using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatBug.WebUI.ViewModels.User;

namespace WhatBug.WebUI.ViewComponents
{
    public class UserSelectorComponentViewModel
    {
        public int SelectedUserId { get; set; }
        public IList<UserViewModel> Users { get; set; }
    }
}
