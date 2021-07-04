using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhatBug.WebUI.ViewModels.Issue
{
    public class CreateIssueViewModel
    {
        [HiddenInput]
        public int ProjectId { get; set; }
        public IssueViewModel Issue { get; set; }
    }
}
