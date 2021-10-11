﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
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
            var projectsUsingScheme = await _context.Projects.Where(p => p.PermissionSchemeId == request.SchemeId).ToListAsync();
            var defaultScheme = await _context.PermissionSchemes.FirstAsync(s => s.IsDefault);

            projectsUsingScheme.ForEach(p => p.PermissionScheme = defaultScheme);

            _context.PermissionSchemes.Remove(scheme);
            await _context.SaveChangesAsync();

            return Response.Success();
        }
    }
}