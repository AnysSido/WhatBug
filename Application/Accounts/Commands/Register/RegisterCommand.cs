using MediatR;

namespace WhatBug.Application.Accounts.Commands.Register
{
    public class RegisterCommand : IRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
