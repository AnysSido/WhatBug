namespace WhatBug.Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        public int Id { get; }
        public string Username { get; }
        public string Email { get; }
        public string FirstName { get; }
        public string Surname { get; }
        public bool IsAuthenticated { get; }
        bool IsReadOnly { get; }
    }
}
