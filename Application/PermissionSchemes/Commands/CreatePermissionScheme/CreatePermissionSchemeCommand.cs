using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Application.Common.MediatR;
using WhatBug.Application.Common.Security;
using WhatBug.Domain.Data;
using WhatBug.Domain.Entities;

namespace WhatBug.Application.PermissionSchemes.Commands.CreatePermissionScheme
{
    [Authorize(Permissions.ManagePermissionSchemes)]
    public record CreatePermissionSchemeCommand : ICommand<Response<int>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class CreatePermissionSchemeCommandHandler : IRequestHandler<CreatePermissionSchemeCommand, Response<int>>
    {
        private readonly IWhatBugDbContext _context;

        public CreatePermissionSchemeCommandHandler(IWhatBugDbContext context)
        {
            _context = context;
        }

        public async Task<Response<int>> Handle(CreatePermissionSchemeCommand request, CancellationToken cancellationToken)
        {
            var permissionScheme = new PermissionScheme { Name = request.Name, Description = request.Description };
            _context.PermissionSchemes.Add(permissionScheme);

            await _context.SaveChangesAsync(cancellationToken);

            return Response<int>.Success(permissionScheme.Id);
        }
    }
}