using System.Collections.Generic;

namespace WhatBug.Application.PrioritySchemes.Queries.GetCreatePriorityScheme
{
    public class CreatePrioritySchemeDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IList<PriorityDTO> Priorities { get; set; }
    }
}