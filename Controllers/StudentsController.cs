using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystem.Models;
using System.Diagnostics;

public class StudentsController : Controller
{
    private readonly AppDbContext _context;

    public StudentsController(AppDbContext context)
    {
        _context = context;
    }

    // List all students
    public async Task<IActionResult> Index()
    {
        try
        {
            var students = await _context.GetStudentsAsync();
            return View(students);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

    // Display the create form
    public IActionResult Create()
    {
        return View();
    }

    // Handle student creation
    [HttpPost]
    public async Task<IActionResult> Create([Bind("Name,Address,DateOfBirth,Email,ContactNumber")] Student student)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await _context.AddStudentAsync(student.Name, student.Address, student.DateOfBirth, student.Email, student.ContactNumber);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        return View(student);
    }

    // Display the edit form
    public async Task<IActionResult> Edit(int id)
    {
        var student = await _context.Students.FindAsync(id);
        if (student == null)
        {
            return NotFound();
        }
        return View(student);
    }

    // Handle student updates
    [HttpPost]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Address,DateOfBirth,Email,ContactNumber")] Student student)
    {
        if (id != student.Id)
        {
            return BadRequest("Student ID mismatch.");
        }

        if (ModelState.IsValid)
        {
            try
            {
                await _context.UpdateStudentAsync(student.Id, student.Name, student.Address, student.DateOfBirth, student.Email, student.ContactNumber);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        return View(student);
    }

    // Handle student deletion
    public async Task<IActionResult> Delete(int id)
    {
        var student = await _context.Students.FindAsync(id);
        if (student == null)
        {
            return NotFound();
        }

        try
        {
            await _context.DeleteStudentAsync(id);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
