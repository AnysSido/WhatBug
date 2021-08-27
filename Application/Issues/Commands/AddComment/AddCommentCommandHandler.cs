using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Application.Common.Result;
using WhatBug.Domain.Entities;

namespace WhatBug.Application.Issues.Commands.AddComment
{
    public class AddCommentCommandHandler : IRequestHandler<AddCommentCommand, Result>
    {
        private readonly IWhatBugDbContext _context;

        public AddCommentCommandHandler(IWhatBugDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(AddCommentCommand request, CancellationToken cancellationToken)
        {
            // TODO: Check permissions
            var author = await _context.Users.FirstOrDefaultAsync(u => u.Id == request.AuthorId);
            if (author == null)
                return Result.Failure(Errors.Issues.CommendAuthorNotFound(request.IssueId, request.AuthorId));

            var issue = await _context.Issues.FirstOrDefaultAsync(i => i.Id == request.IssueId);
            if (issue == null)
                return Result.Failure(Errors.Issues.IssueNotFound(request.IssueId));

            var comment = new IssueComment
            {
                Content = request.Content,
                Author = author,
                Issue = issue,
                Timestamp = DateTime.Now
            };

            _context.IssueComments.Add(comment);
            await _context.SaveChangesAsync();

            return Result.Success();
        }
    }
}
