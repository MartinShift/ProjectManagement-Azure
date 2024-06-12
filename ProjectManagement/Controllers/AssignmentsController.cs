using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Business.Models;
using ProjectManagement.Business.Services.Interfaces;
using ProjectManagement.Data;

namespace ProjectManagement.Controllers;

[Authorize]
public class AssignmentsController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly IAssignmentService _assignmentService;

    public AssignmentsController(UserManager<User> userManager, IAssignmentService assignmentService)
    {
        _userManager = userManager;
        _assignmentService = assignmentService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var user = await _userManager.GetUserAsync(User);
        var assignments = await _assignmentService.GetByUserAsync(user.Id);
        return View(assignments);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(AssignmentDto assignment)
    {
        if (ModelState.IsValid)
        {
            await _assignmentService.AddAsync(assignment);
            return RedirectToAction(nameof(Index));
        }
        return View(assignment);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(string id)
    {
        var assignment = await _assignmentService.GetByIdAsync(id);
        if (assignment == null)
        {
            return NotFound();
        }
        return View(assignment);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(string id, AssignmentDto assignment)
    {
        if (id != assignment.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            await _assignmentService.UpdateAsync(id, assignment);
            return RedirectToAction(nameof(Index));
        }
        return View(assignment);
    }

    [HttpGet]
    public async Task<IActionResult> Delete(string id)
    {
        var assignment = await _assignmentService.GetByIdAsync(id);
        if (assignment == null)
        {
            return NotFound();
        }
        return View(assignment);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
        await _assignmentService.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}