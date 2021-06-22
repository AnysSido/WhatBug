namespace WhatBug.Domain.Entities.Permissions
{
    public enum PermissionType { Global, Project, Issue }

    public class Permission // Create Issue etc
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public PermissionType Type { get; set; } // Global/Project/Issue

        public Permission(int id, string name, string description, PermissionType type)
        {
            Id = id;
            Name = name;
            Description = description;
            Type = type;
        }
    }
}
