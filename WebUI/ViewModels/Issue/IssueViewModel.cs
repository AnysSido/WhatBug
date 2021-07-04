using System.ComponentModel.DataAnnotations;

namespace WhatBug.WebUI.ViewModels.Issue
{
    public class IssueViewModel
    {
        [Required]
        [Display(Name = "Issue Name", Prompt = "My Issue")]
        public string Name { get; set; }

        [Display(Name = "Issue Description")]
        public string Description { get; set; }
    }
}
