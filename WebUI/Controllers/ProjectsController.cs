using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WhatBug.Application.Common.Interfaces;
using WhatBug.Application.DTOs.Projects;
using WhatBug.Application.Services.Interfaces;
using WhatBug.Domain.Entities;
using WhatBug.Persistence;
using WhatBug.WebUI.ViewModels.PrioritySchemes;
using WhatBug.WebUI.ViewModels.Projects;

namespace WhatBug.WebUI.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly IProjectService _projectService;
        private readonly ICurrentUserService _currentUserService;
        private readonly IPrioritySchemeService _prioritySchemeService;
        private readonly IMapper _mapper;

        public ProjectsController(IProjectService projectService, ICurrentUserService currentUserService, IPrioritySchemeService prioritySchemeService, IMapper mapper)
        {
            _projectService = projectService;
            _currentUserService = currentUserService;
            _prioritySchemeService = prioritySchemeService;
            _mapper = mapper;
        }

        // GET: Projects
        public async Task<IActionResult> Index()
        {
            return View(await _projectService.ListProjects());
        }

        // GET: Projects/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var project = await _context.Projects
        //        .FirstOrDefaultAsync(m => m.ProjectId == id);
        //    if (project == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(project);
        //}

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var vm = new CreateProjectViewModel()
            {
                PrioritySchemes = _mapper.Map<List<PrioritySchemeViewModel>>(await _prioritySchemeService.GetPrioritySchemesAsync())
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateProjectViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.PrioritySchemes = _mapper.Map<List<PrioritySchemeViewModel>>(await _prioritySchemeService.GetPrioritySchemesAsync());
                return View(vm);
            }

            await _projectService.CreateProject(_mapper.Map<CreateProjectDTO>(vm));
            return RedirectToAction(nameof(Index));
        }

        // GET: Projects/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var project = await _context.Projects.FindAsync(id);
        //    if (project == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(project);
        //}

        // POST: Projects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("ProjectId,Name,Description")] Project project)
        //{
        //    if (id != project.ProjectId)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(project);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!ProjectExists(project.ProjectId))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(project);
        //}

        // GET: Projects/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var project = await _context.Projects
        //        .FirstOrDefaultAsync(m => m.ProjectId == id);
        //    if (project == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(project);
        //}

        // POST: Projects/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var project = await _context.Projects.FindAsync(id);
        //    _context.Projects.Remove(project);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}
    }
}
