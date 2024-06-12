using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Business.Models;
using ProjectManagement.Business.Services.Interfaces;
using ProjectManagement.Data;

namespace ProjectManagement.Controllers;

[Authorize]
public class ProjectsController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly IProjectService _projectService;
    public ProjectsController(UserManager<User> userManager, IProjectService projectService)
    {
        _userManager = userManager;
        _projectService = projectService;
    }
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var user = await _userManager.GetUserAsync(User);
        var projects = await _projectService.GetByUserAsync(user.Id);
        return View(projects);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProjectDto project)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.GetUserAsync(User);
            project.Members = new List<User> { user };
            await _projectService.AddAsync(project);
            return RedirectToAction(nameof(Index));
        }
        return View(project);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(string id)
    {
        var project = await _projectService.GetByIdAsync(id);
        if (project == null)
        {
            return NotFound();
        }
        return View(project);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(string id, ProjectDto project)
    {
        if (id != project.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            await _projectService.UpdateAsync(id, project);
            return RedirectToAction(nameof(Index));
        }
        return View(project);
    }

    [HttpGet]
    public async Task<IActionResult> Delete(string id)
    {
        var project = await _projectService.GetByIdAsync(id);
        if (project == null)
        {
            return NotFound();
        }
        return View(project);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
        await _projectService.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
