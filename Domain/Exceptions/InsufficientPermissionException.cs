using System;
using WhatBug.Domain.Entities.Permissions;

namespace WhatBug.Domain.Exceptions
{
    [Serializable]
    public class InsufficientPermissionException : Exception
    {
        public int UserId { get; set; }
        public int PermissionId { get;set; }

        public InsufficientPermissionException() { }

        public InsufficientPermissionException(string message)
        : base(message) { }

        public InsufficientPermissionException(string message, Exception inner)
            : base(message, inner) { }

        public InsufficientPermissionException(int userId, Permission permission)
            : base($"User ({userId}) lacks required permission ({permission.Name}).")
        {
            UserId = userId;
            PermissionId = permission.Id;
        }

        public InsufficientPermissionException(int userId, Permission permission, Exception inner)
            : base($"User ({userId}) lacks required permission ({permission.Name}).", inner)
        {
            UserId = userId;
            PermissionId = permission.Id;
        }
    }
}
