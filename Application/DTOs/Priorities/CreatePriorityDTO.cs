﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatBug.Application.DTOs.Priorities
{
    public class CreatePriorityDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public string PriorityIconName { get; set; }
    }
}