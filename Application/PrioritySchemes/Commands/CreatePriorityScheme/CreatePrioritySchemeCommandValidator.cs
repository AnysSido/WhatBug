using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatBug.Application.PrioritySchemes.Commands.CreatePriorityScheme
{
    public class CreatePrioritySchemeCommandValidator : AbstractValidator<CreatePrioritySchemeCommand>
    {
        public CreatePrioritySchemeCommandValidator()
        {
            RuleFor(v => v.Name).NotEmpty();
            RuleFor(v => v.Description).NotEmpty();
        }
    }
}
