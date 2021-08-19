using System.ComponentModel.DataAnnotations;

namespace WhatBug.WebUI.Features.Accounts.Register
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Username", Prompt = "Username")]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email", Prompt = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password", Prompt = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password", Prompt = "Retype Password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Range(typeof(bool), "true", "true", ErrorMessage = "You must agree to the terms.")]
        public bool AgreeToTerms { get; set; }
    }
}
