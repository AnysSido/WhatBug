namespace WhatBug.Domain.Entities
{
    public class ProjectRoleUser
    {
        public int ProjectId { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }

        public Project Project { get; set; }
        public User User { get; set; }
        public Role Role { get; set; }
    }
}
