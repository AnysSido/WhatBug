using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Exceptions;
using WhatBug.Application.Common.Extensions;
using WhatBug.Application.Common.Interfaces;

namespace WhatBug.Application.Projects.Commands.UnassignUsersFromRole
{
    public class UnassignUsersFromRoleCommandValidator : AbstractValidator<UnassignUsersFromRoleCommand>
    {
        private IWhatBugDbContext _context;

        public UnassignUsersFromRoleCommandValidator(IWhatBugDbContext context)
        {
            _context = context;

            RuleFor(v => v.ProjectId)
                .GreaterThan(0).WithException(query => new ArgumentException(nameof(query.ProjectId)))
                .MustAsync(ProjectExist).WithException(query => new RecordNotFoundException());

            RuleFor(v => v.RoleId)
                .GreaterThan(0).WithException(query => new ArgumentException(nameof(query.RoleId)))
                .MustAsync(RoleExist).WithException(query => new RecordNotFoundException());
        }

        public async Task<bool> ProjectExist(UnassignUsersFromRoleCommand query, int projectId, CancellationToken cancellationToken)
        {
            return await _context.Projects.AnyAsync(p => p.Id == projectId);
        }

        public async Task<bool> RoleExist(UnassignUsersFromRoleCommand query, int roleId, CancellationToken cancellationToken)
        {
            return await _context.Roles.AnyAsync(r => r.Id == roleId);
        }
    }
}
