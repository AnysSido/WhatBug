using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatBug.Application.Accounts.Commands.Register
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            // TODO: Add rules
            RuleFor(v => v.Username).NotEmpty();
            RuleFor(v => v.Email).NotEmpty().EmailAddress();
            RuleFor(v => v.Password).NotEmpty();
        }
    }
}
