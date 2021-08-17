using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatBug.Application.Priorities.Commands.ReorderPriorities
{
    public class ReorderPrioritiesCommandValidator : AbstractValidator<ReorderPrioritiesCommand>
    {
        public ReorderPrioritiesCommandValidator()
        {
            RuleFor(v => v.Ids).NotNull().NotEmpty();
        }
    }
}
