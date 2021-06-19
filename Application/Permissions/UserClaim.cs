using WhatBug.Domain.Entities;

namespace WhatBug.Application.Permissions
{
    public class UserClaim
    {
        public int UserClaimId { get; set; }
        public int UserId { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
        public User User { get; set; }
    }
}
