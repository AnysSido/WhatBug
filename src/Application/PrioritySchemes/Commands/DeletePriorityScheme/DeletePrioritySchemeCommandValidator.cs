using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Exceptions;
using WhatBug.Application.Common.Extensions;
using WhatBug.Application.Common.Interfaces;

namespace WhatBug.Application.PrioritySchemes.Commands.DeletePriorityScheme
{
    public class DeletePrioritySchemeCommandValidator : AbstractValidator<DeletePrioritySchemeCommand>
    {
        private IWhatBugDbContext _context;

        public DeletePrioritySchemeCommandValidator(IWhatBugDbContext context)
        {
            _context = context;

            RuleFor(v => v.SchemeId)
                .Cascade(CascadeMode.Stop)
                .GreaterThan(0).WithException(cmd => new ArgumentException(nameof(cmd.SchemeId)))
                .MustAsync(Exist).WithException(cmd => new RecordNotFoundException())
                .MustAsync(NotBeDefault).WithException(cmd => new ArgumentException(nameof(cmd.SchemeId)));
        }

        public async Task<bool> Exist(DeletePrioritySchemeCommand command, int schemeId, CancellationToken cancellationToken)
        {
            return await _context.PrioritySchemes.AnyAsync(s => s.Id == schemeId);
        }

        public async Task<bool> NotBeDefault(DeletePrioritySchemeCommand command, int schemeId, CancellationToken cancellationToken)
        {
            return !await _context.PrioritySchemes.AnyAsync(s => s.Id == schemeId && s.IsDefault);
        }
    }
}