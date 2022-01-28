using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Application.Common.MediatR;
using WhatBug.Application.Common.Security;
using WhatBug.Domain.Data;
using WhatBug.Domain.Entities;

namespace WhatBug.Application.Projects.Commands.CreateProject
{
    [Authorize(Permissions.CreateProject)]
    public record CreateProjectCommand : ICommand<Response<int>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Key { get; set; }
        public int PrioritySchemeId { get; set; }
        public int PermissionSchemeId { get; set; }
    }

    public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, Response<int>>
    {
        private readonly IWhatBugDbContext _context;

        public CreateProjectCommandHandler(IWhatBugDbContext context)
        {
            _context = context;
        }

        public async Task<Response<int>> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = new Project
            {
                Name = request.Name,
                Description = request.Description,
                Key = request.Key,
                PrioritySchemeId = request.PrioritySchemeId,
                PermissionSchemeId = request.PermissionSchemeId,
                IssueCounter = 0
            };

            _context.Projects.Add(project);
            await _context.SaveChangesAsync(cancellationToken);

            return Response<int>.Success(project.Id);
        }
    }
}
