using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatBug.Infrastructure.Identity
{
    public class PrincipalUser : IdentityUser<int>
    {
        public int UserId { get; set; }
    }
}
