using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatBug.Domain.Entities
{
    public class IssueType
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int IconId { get; set; }
        public Icon Icon{ get; set; }
    }
}
