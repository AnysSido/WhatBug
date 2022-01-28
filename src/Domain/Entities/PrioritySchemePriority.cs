namespace WhatBug.Domain.Entities
{
    public class PrioritySchemePriority
    {
        public int PrioritySchemeId { get; set; }
        public PriorityScheme PriorityScheme { get; set; }

        public int PriorityId { get; set; }
        public Priority Priority { get; set; }
    }
}