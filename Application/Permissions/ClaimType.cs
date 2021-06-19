namespace WhatBug.Application.Permissions
{
    public sealed class ClaimType
    {
        public static implicit operator string(ClaimType op) { return op.Value; }

        public string Value { get; private set; }

        private ClaimType(string value)
        {
            Value = value;
        }

        public sealed class Project
        {
            public static readonly ClaimType Add = new ClaimType("ProjectAdd");
        }
    }
}
