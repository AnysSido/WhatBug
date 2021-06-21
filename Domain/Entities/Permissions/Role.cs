namespace WhatBug.Domain.Entities.Permissions
{
    public enum RoleType { Global, Scheme }

    public class Role // Global (Admin, User) | Scheme (Administrator, Developer, QA, Viewer)
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public RoleType Type { get; set; } // Global or scheme
    }
}