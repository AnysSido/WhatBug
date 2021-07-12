using System.ComponentModel.DataAnnotations;

namespace WhatBug.WebUI.ViewModels.Issues
{
    public class IssueViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Summary { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
