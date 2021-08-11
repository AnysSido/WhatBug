using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatBug.Application.PrioritySchemes.Commands.EditPriorityScheme
{
    public class EditPrioritySchemeCommandValidator : AbstractValidator<EditPrioritySchemeCommand>
    {
        public EditPrioritySchemeCommandValidator()
        {
            RuleFor(v => v.Name).NotEmpty();
            RuleFor(v => v.Description).NotEmpty();
        }
    }
}
