using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatBug.Domain.Entities.Priorities
{
    public class Priority
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
        
        public int ColorIconId { get; set; }
        public ColorIcon ColorIcon{ get; set; }

        public List<PriorityScheme> PrioritySchemes { get; set; }
    }
}
