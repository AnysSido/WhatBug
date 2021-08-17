namespace WhatBug.Domain.Entities
{
    public class IssueType
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int IconId { get; set; }
        public Icon Icon { get; set; }

        public int ColorId { get; set; }
        public Color Color { get; set; }
    }
}
