namespace WhatBug.Domain.Entities.Permissions
{
    public class ProjectRoleUser
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }

        public Project Project { get; set; }
        public User User { get; set; }
    }
}
