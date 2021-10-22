using System.Collections.Generic;

namespace WhatBug.Domain.Entities
{
    public class PriorityScheme
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDefault { get; set; }

        public List<PrioritySchemePriority> Priorities { get; set; }
        public List<Project> Projects { get; set; }
    }
}