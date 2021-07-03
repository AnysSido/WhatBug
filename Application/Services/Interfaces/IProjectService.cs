﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatBug.Application.Common.Models;
using WhatBug.Application.DTOs.Projects;
using WhatBug.Domain.Entities;

namespace WhatBug.Application.Services.Interfaces
{
    public interface IProjectService
    {
        public Task CreateProject(CreateProjectDTO createProjectDTO);
        Task<List<Project>> ListProjects();
    }
}