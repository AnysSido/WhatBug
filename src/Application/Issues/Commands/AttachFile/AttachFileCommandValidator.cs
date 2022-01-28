using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Exceptions;
using WhatBug.Application.Common.Extensions;
using WhatBug.Application.Common.Interfaces;

namespace WhatBug.Application.Issues.Commands.AttachFile
{
    public class AttachFileCommandValidator : AbstractValidator<AttachFileCommand>
    {
        private IWhatBugDbContext _context;

        public AttachFileCommandValidator(IWhatBugDbContext context)
        {
            _context = context;

            RuleFor(v => v.IssueId)
                .NotEmpty().WithException(cmd => new ArgumentException(nameof(cmd.IssueId)))
                .MustAsync(IssueExist).WithException(cmd => new RecordNotFoundException());

            RuleFor(v => v.FileName)
                .NotEmpty().WithException(cmd => new ArgumentException(nameof(cmd.FileName)));

            RuleFor(v => v.File)
                .NotEmpty().WithException(cmd => new ArgumentException(nameof(cmd.File)));

            RuleFor(v => v.ContentType)
                .NotEmpty().WithException(cmd => new ArgumentException(nameof(cmd.ContentType)));
        }

        public async Task<bool> IssueExist(AttachFileCommand command, string issueId, CancellationToken cancellationToken)
        {
            return await _context.Issues.AnyAsync(i => i.Id == issueId);
        }
    }
}
