namespace WhatBug.Domain.Entities
{
    public enum PermissionType { Global, Project, Issue }

    public class Permission
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public PermissionType Type { get; set; }
    }
}
