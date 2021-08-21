using System.Collections.Generic;

namespace WhatBug.Domain.Entities
{
    public class Priority
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }

        public int ColorId { get; set; }
        public Color Color { get; set; }

        public int IconId { get; set; }
        public Icon Icon { get; set; }

        public List<PriorityScheme> PrioritySchemes { get; set; }
    }
}
