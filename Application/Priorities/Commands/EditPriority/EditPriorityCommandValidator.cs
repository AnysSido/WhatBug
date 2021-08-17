using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatBug.Application.Priorities.Commands.EditPriority
{
    public class EditPriorityCommandValidator : AbstractValidator<EditPriorityCommand>
    {
        public EditPriorityCommandValidator()
        {
            RuleFor(v => v.Id).NotEmpty();
            RuleFor(v => v.Name).NotEmpty();
            RuleFor(v => v.Description).NotEmpty();
            RuleFor(v => v.ColorId).NotEmpty();
            RuleFor(v => v.IconId).NotEmpty();
        }
    }
}
