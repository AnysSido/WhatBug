using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Application.Common.MediatR;
using WhatBug.Application.Common.Security;
using WhatBug.Domain.Data;

namespace WhatBug.Application.PermissionSchemes.Commands.EditPermissionScheme
{
    [Authorize(Permissions.ManagePermissionSchemes)]
    public record EditPermissionSchemeCommand : ICommand<Response>
    {
        public int SchemeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class EditPermissionSchemeCommandHandler : IRequestHandler<EditPermissionSchemeCommand, Response>
    {
        private readonly IWhatBugDbContext _context;

        public EditPermissionSchemeCommandHandler(IWhatBugDbContext context)
        {
            _context = context;
        }

        public async Task<Response> Handle(EditPermissionSchemeCommand request, CancellationToken cancellationToken)
        {
            var scheme = await _context.PermissionSchemes.Where(s => s.Id == request.SchemeId).FirstOrDefaultAsync();

            scheme.Name = request.Name;
            scheme.Description = !string.IsNullOrEmpty(request.Description) ? request.Description : null;

            await _context.SaveChangesAsync();

            return Response.Success();
        }
    }
}
