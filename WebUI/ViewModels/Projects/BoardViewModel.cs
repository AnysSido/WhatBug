using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhatBug.WebUI.ViewModels.Projects
{
    public class BoardViewModel
    {
        public List<string> Columns = new List<string>() { "Backlog", "ToDo", "In Progress", "Done" };
    }
}
