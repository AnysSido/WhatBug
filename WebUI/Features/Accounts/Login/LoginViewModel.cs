using System.ComponentModel.DataAnnotations;

namespace WhatBug.WebUI.Features.Accounts.Login
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "You must enter an email address or username.")]
        [Display(Name = "Username", Prompt = "Email or username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "You must enter a password.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password", Prompt = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

        public bool RegistrationEnabled { get; set; }
        public bool DemoEnabled { get; set; }
    }
}
