using System.Collections.Generic;

namespace WhatBug.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string JobTitle { get; set; }
        public string Department { get; set; }
        public string Organization { get; set; }
        public string Location { get; set; }

        public List<ProjectRoleUser> ProjectRoles { get; set; }
        public List<Issue> AssignedIssues { get; set; }
        public List<Issue> ReportedIssues { get; set; }
        public List<UserPermission> UserPermissions { get; set; }
    }
}
