using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Exceptions;
using WhatBug.Application.Common.Extensions;
using WhatBug.Application.Common.Interfaces;

namespace WhatBug.Application.Issues.Commands.CreateIssue
{
    public class CreateIssueCommandValidator : AbstractValidator<CreateIssueCommand>
    {
        private IWhatBugDbContext _context;

        public CreateIssueCommandValidator(IWhatBugDbContext context)
        {
            _context = context;

            RuleFor(v => v.Summary)
                .NotEmpty().WithMessage("Issue summary cannot be empty");

            RuleFor(v => v.ProjectId)
                .GreaterThan(0).WithException(query => new ArgumentException(nameof(query.ProjectId)))
                .MustAsync(ProjectExist).WithException(query => new RecordNotFoundException());

            RuleFor(v => v.PriorityId)
                .GreaterThan(0).WithException(query => new ArgumentException(nameof(query.PriorityId)))
                .MustAsync(PriorityExist).WithException(query => new RecordNotFoundException());

            RuleFor(v => v.IssueTypeId)
                .GreaterThan(0).WithException(query => new ArgumentException(nameof(query.IssueTypeId)))
                .MustAsync(IssueTypeExist).WithException(query => new RecordNotFoundException());

            RuleFor(v => v.AssigneeId)
                .GreaterThan(0).WithException(query => new ArgumentException(nameof(query.AssigneeId)))
                .MustAsync(UserExist).WithException(query => new RecordNotFoundException());

            RuleFor(v => v.ReporterId)
                .GreaterThan(0).WithException(query => new ArgumentException(nameof(query.ReporterId)))
                .MustAsync(UserExist).WithException(query => new RecordNotFoundException());
        }

        public async Task<bool> ProjectExist(CreateIssueCommand query, int projectId, CancellationToken cancellationToken)
        {
            return await _context.Projects.AnyAsync(p => p.Id == projectId);
        }

        public async Task<bool> PriorityExist(CreateIssueCommand query, int priorityId, CancellationToken cancellationToken)
        {
            return await _context.Priorities.AnyAsync(p => p.Id == priorityId);
        }

        public async Task<bool> IssueTypeExist(CreateIssueCommand query, int issueTypeId, CancellationToken cancellationToken)
        {
            return await _context.IssueTypes.AnyAsync(p => p.Id == issueTypeId);
        }

        public async Task<bool> UserExist(CreateIssueCommand query, int userId, CancellationToken cancellationToken)
        {
            return await _context.Users.AnyAsync(p => p.Id == userId);
        }
    }
}
