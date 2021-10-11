using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Exceptions;
using WhatBug.Application.Common.Extensions;
using WhatBug.Application.Common.Interfaces;

namespace WhatBug.Application.PermissionSchemes.Commands.DeletePermissionScheme
{
    public class DeletePermissionSchemeCommandValidator : AbstractValidator<DeletePermissionSchemeCommand>
    {
        private IWhatBugDbContext _context;

        public DeletePermissionSchemeCommandValidator(IWhatBugDbContext context)
        {
            _context = context;

            RuleFor(v => v.SchemeId)
                .Cascade(CascadeMode.Stop)
                .GreaterThan(0).WithException(cmd => new ArgumentException(nameof(cmd.SchemeId)))
                .MustAsync(Exist).WithException(cmd => new RecordNotFoundException())
                .MustAsync(NotBeDefault).WithException(cmd => new ArgumentException(nameof(cmd.SchemeId)));
        }

        public async Task<bool> Exist(DeletePermissionSchemeCommand command, int schemeId, CancellationToken cancellationToken)
        {
            return await _context.PermissionSchemes.AnyAsync(s => s.Id == schemeId);
        }

        public async Task<bool> NotBeDefault(DeletePermissionSchemeCommand command, int schemeId, CancellationToken cancellationToken)
        {
            return !await _context.PermissionSchemes.AnyAsync(s => s.Id == schemeId && s.IsDefault);
        }
    }
}