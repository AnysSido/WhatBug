using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Exceptions;
using WhatBug.Application.Common.Extensions;
using WhatBug.Application.Common.Interfaces;

namespace WhatBug.Application.Projects.Commands.CreateProject
{
    public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
    {
        private IWhatBugDbContext _context;

        public CreateProjectCommandValidator(IWhatBugDbContext context)
        {
            _context = context;

            RuleFor(v => v.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Project name cannot be empty")
                .MustAsync(NameUnique).WithMessage(cmd => $"A project with the name {cmd.Name} already exists");

            RuleFor(v => v.Key)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Key cannot be empty")
                .Length(2, 6).WithMessage("Key must be between 2 and 6 characters and contain only capital letters")
                .Matches("^[A-Z]+$").WithMessage("Key must be between 2 and 6 characters and contain only capital letters")
                .MustAsync(KeyUnique).WithMessage(cmd => $"The key {cmd.Key} is already in use");

            RuleFor(v => v.PrioritySchemeId)
                .Cascade(CascadeMode.Stop)
                .GreaterThan(0).WithException(cmd => new ArgumentException(nameof(cmd.PrioritySchemeId)))
                .MustAsync(PrioritySchemeExist).WithException(cmd => new RecordNotFoundException());

            RuleFor(v => v.PermissionSchemeId)
                .Cascade(CascadeMode.Stop)
                .GreaterThan(0).WithException(cmd => new ArgumentException(nameof(cmd.PermissionSchemeId)))
                .MustAsync(PermissionSchemeExist).WithException(cmd => new RecordNotFoundException());
        }

        public async Task<bool> NameUnique(CreateProjectCommand command, string name, CancellationToken cancellationToken)
        {
            return !await _context.Projects.AnyAsync(p => p.Name == name);
        }

        public async Task<bool> KeyUnique(CreateProjectCommand command, string key, CancellationToken cancellationToken)
        {
            return !await _context.Projects.AnyAsync(p => p.Key == key);
        }

        public async Task<bool> PrioritySchemeExist(CreateProjectCommand command, int id, CancellationToken cancellationToken)
        {
            return await _context.PrioritySchemes.AnyAsync(p => p.Id == id);
        }

        public async Task<bool> PermissionSchemeExist(CreateProjectCommand command, int id, CancellationToken cancellationToken)
        {
            return await _context.PermissionSchemes.AnyAsync(p => p.Id == id);
        }
    }
}
