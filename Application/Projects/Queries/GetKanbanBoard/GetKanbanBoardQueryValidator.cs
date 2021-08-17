using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatBug.Application.Projects.Queries.GetKanbanBoard
{
    public class GetKanbanBoardQueryValidator : AbstractValidator<GetKanbanBoardQuery>
    {
        public GetKanbanBoardQueryValidator()
        {
            RuleFor(v => v.ProjectId).NotEmpty();
        }
    }
}
