using System;
using WhatBug.Domain.Entities.Permissions;

namespace WhatBug.Domain.Exceptions
{
    [Serializable]
    public class InsufficientPermissionException : Exception
    {
        public int UserId { get; set; }
        public string Permission { get;set; }

        public InsufficientPermissionException(int userId, string permission)
            : base($"User ({userId}) lacks required permission ({permission}).")
        {
            UserId = userId;
            Permission = permission;
        }

        public InsufficientPermissionException(int userId, string permission, Exception inner)
            : base($"User ({userId}) lacks required permission ({permission}).", inner)
        {
            UserId = userId;
            Permission = permission;
        }
    }
}
