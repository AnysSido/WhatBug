using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace WhatBug.Infrastructure.Identity
{
    public class PrincipalUser : IdentityUser<int>
    {
        public int UserId { get; set; }

        [NotMapped]
        public bool IsReadOnly { get; set; }
    }
}