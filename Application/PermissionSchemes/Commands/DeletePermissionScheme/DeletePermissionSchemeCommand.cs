using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Application.Common.MediatR;
using WhatBug.Application.Common.Security;
using WhatBug.Domain.Data;

namespace WhatBug.Application.PermissionSchemes.Commands.DeletePermissionScheme
{
    [Authorize(Permissions.ManagePermissionSchemes)]
    public record DeletePermissionSchemeCommand : ICommand<Response>
    {
        public int SchemeId { get; init; }
    }

    public class DeletePermissionSchemeCommandHandler : IRequestHandler<DeletePermissionSchemeCommand, Response>
    {
        private readonly IWhatBugDbContext _context;

        public DeletePermissionSchemeCommandHandler(IWhatBugDbContext context)
        {
            _context = context;
        }

        public async Task<Response> Handle(DeletePermissionSchemeCommand request, CancellationToken cancellationToken)
        {
            var scheme = await _context.PermissionSchemes.FirstAsync(s => s.Id == request.SchemeId);

            // TODO: Reassign projects to default

            _context.PermissionSchemes.Remove(scheme);
            await _context.SaveChangesAsync();

            return Response.Success();
        }
    }
}