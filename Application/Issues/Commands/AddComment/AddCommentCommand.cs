using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Application.Common.MediatR;
using WhatBug.Application.Common.Security;
using WhatBug.Domain.Data;
using WhatBug.Domain.Entities;

namespace WhatBug.Application.Issues.Commands.AddComment
{
    [Authorize(Permissions.Comment)]
    public record AddCommentCommand : ICommand<Response>, IRequireIssueAuthorization
    {
        public string IssueId { get; init; }
        public string Content { get; init; }
    }

    public class AddCommentCommandHandler : IRequestHandler<AddCommentCommand, Response>
    {
        private readonly IWhatBugDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public AddCommentCommandHandler(IWhatBugDbContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<Response> Handle(AddCommentCommand request, CancellationToken cancellationToken)
        {
            var author = await _context.Users.FirstOrDefaultAsync(u => u.Id == _currentUserService.Id);
            var issue = await _context.Issues.FirstOrDefaultAsync(i => i.Id == request.IssueId);

            var comment = new IssueComment
            {
                Content = request.Content,
                Author = author,
                Issue = issue,
                Timestamp = DateTime.Now
            };

            _context.IssueComments.Add(comment);
            await _context.SaveChangesAsync();

            return Response.Success();
        }
    }
}
