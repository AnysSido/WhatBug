using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Exceptions;
using WhatBug.Application.Common.Extensions;
using WhatBug.Application.Common.Interfaces;

namespace WhatBug.Application.Priorities.Commands.DeletePriority
{
    public class DeletePriorityCommandValidator : AbstractValidator<DeletePriorityCommand>
    {
        private IWhatBugDbContext _context;

        public DeletePriorityCommandValidator(IWhatBugDbContext context)
        {
            _context = context;

            RuleFor(v => v.PriorityId)
                .Cascade(CascadeMode.Stop)
                .GreaterThan(0).WithException(cmd => new ArgumentException(nameof(cmd.PriorityId)))
                .MustAsync(Exist).WithException(cmd => new RecordNotFoundException())
                .MustAsync(NotBeDefault).WithException(cmd => new ArgumentException(nameof(cmd.PriorityId)));
        }

        public async Task<bool> Exist(DeletePriorityCommand command, int priorityId, CancellationToken cancellationToken)
        {
            return await _context.Priorities.AnyAsync(s => s.Id == priorityId);
        }

        public async Task<bool> NotBeDefault(DeletePriorityCommand command, int priorityId, CancellationToken cancellationToken)
        {
            return !await _context.Priorities.AnyAsync(s => s.Id == priorityId && s.IsDefault);
        }
    }
}