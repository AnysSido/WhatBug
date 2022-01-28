using System;

namespace WhatBug.Domain.Exceptions
{
    [Serializable]
    public class UserNotFoundException : Exception
    {
        public int UserId { get; set; }

        public UserNotFoundException() { }

        public UserNotFoundException(string message)
        : base(message) { }

        public UserNotFoundException(string message, Exception inner)
            : base(message, inner) { }

        public UserNotFoundException(int userId)
            : base($"User {userId} does not exist.") 
        {
            UserId = userId;
        }

        public UserNotFoundException(int userId, Exception inner)
            : base($"User {userId} does not exist.", inner)
        {
            UserId = userId;
        }
    }
}
