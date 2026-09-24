using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Application.Common.MediatR;
using WhatBug.Application.Common.Security;
using WhatBug.Application.Common.Settings;
using WhatBug.Domain.Entities;

namespace WhatBug.Application.Accounts.Commands.Register
{
    [NoAuthorize]
    public record RegisterCommand : ICommand<Response>
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }

    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Response>
    {
        private readonly IWhatBugDbContext _context;
        private readonly IAuthenticationProvider _authenticationProvider;
        private readonly WhatBugSettings _whatbugSettings;

        public RegisterCommandHandler(IWhatBugDbContext context, IAuthenticationProvider authenticationProvider, IOptions<WhatBugSettings> whatbugSettings)
        {
            _context = context;
            _authenticationProvider = authenticationProvider;
            _whatbugSettings = whatbugSettings.Value;
        }

        public async Task<Response> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            if (!_whatbugSettings.Accounts.RegistrationEnabled)
                throw new InvalidOperationException();

            await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
            await _context.Database.ExecuteSqlRawAsync("SELECT pg_advisory_xact_lock(1463964226)", cancellationToken);
            var isFirstUser = !await _context.Users.AnyAsync(cancellationToken);
            var user = new User { Username = request.Username, Email = request.Email };
            if (isFirstUser)
            {
                user.UserPermissions = await _context.Permissions
                    .Where(p => p.Type == PermissionType.Global)
                    .Select(p => new UserPermission { PermissionId = p.Id })
                    .ToListAsync(cancellationToken);
            }

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var success = await _authenticationProvider.CreateUserAsync(request.Username, request.Password, request.Email, user.Id);

            if (!success)
            {
                throw new InvalidOperationException("Unable to create the account.");
            }

            try
            {
                await transaction.CommitAsync(cancellationToken);
            }
            catch (Exception commitException)
            {
                try
                {
                    if (!await _authenticationProvider.DeleteUserAsync(user.Id))
                        throw new InvalidOperationException("Unable to remove the identity after registration failed.");
                }
                catch (Exception cleanupException)
                {
                    throw new AggregateException("Registration failed and identity cleanup failed.", commitException, cleanupException);
                }

                throw;
            }
            return Response.Success();
        }
    }
}
