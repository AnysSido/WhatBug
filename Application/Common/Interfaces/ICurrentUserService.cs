using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatBug.Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        // AuthenticationId is the user Id from an authentication context, provided by the selected authentication provider.
        int AuthenticationId { get; }

        // UserId is the user Id from the application domain context.
        int UserId { get; }
        bool IsAuthenticated { get; }
    }
}
