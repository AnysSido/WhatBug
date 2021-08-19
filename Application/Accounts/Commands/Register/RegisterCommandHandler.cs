using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Domain.Entities;

namespace WhatBug.Application.Accounts.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand>
    {
        private readonly IWhatBugDbContext _context;
        private readonly IAuthenticationProvider _authenticationProvider;

        public RegisterCommandHandler(IWhatBugDbContext context, IAuthenticationProvider authenticationProvider)
        {
            _context = context;
            _authenticationProvider = authenticationProvider;
        }

        public async Task<Unit> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            // TODO: This should be a transaction so we can rollback the user if something happens with principal user creation.

            // First create a new user to get its id.
            var user = new User { Username = request.Username, Email = request.Email };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Then create this user with the authentication provider
            var result = await _authenticationProvider.CreateUserAsync(request.Username, request.Password, request.Email, user.Id);

            if (!result)
            {
                // TODO: Handle this
            }

            return Unit.Value;
        }
    }
}
