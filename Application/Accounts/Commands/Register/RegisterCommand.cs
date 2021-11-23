using MediatR;
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

            var user = new User { Username = request.Username, Email = request.Email };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var success = await _authenticationProvider.CreateUserAsync(request.Username, request.Password, request.Email, user.Id);

            if (!success)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();

                throw new InvalidOperationException(); // TODO: Improve handling
            }

            return Response.Success();
        }
    }
}
