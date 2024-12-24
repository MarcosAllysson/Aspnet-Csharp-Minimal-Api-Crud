using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MinimalApiCrud.Models;
using MinimalApiCrud.Models.Data;

namespace MinimalApiCrud.Students
{
    public static class StudentController
    {
        public static void AddStudentController(this WebApplication app)
        {
            // INITIAL MESSAGE
            // app.MapGet("students", () => "Hi, students!");

            /*
            CancellationToken ct
            Tell EF to warn DB to stop execution in case of its running for a long time.
            All methods using Async, pass "ct"
            */

            // CREATE
            app.MapPost(
                "student",
                async (AddStudent request, AppDbContext context, CancellationToken ct) =>
            {
                var isNameDuplicate = await context.Students.AnyAsync(student => student.Name.ToLower() == request.Name.ToLower());

                if (isNameDuplicate)
                {
                    return Results.Conflict($"Student {request.Name} already created.");
                    // return Results.Unauthorized();
                }

                var newStudent = new Student(request.Name);

                await context.Students.AddAsync(newStudent);
                await context.SaveChangesAsync();

                return Results.Created();
            });

            // GET ALL
            app.MapGet(
                "students",
                async (AppDbContext context) =>
                {
                    var students = await context.Students
                        .AsNoTracking()
                        .Where(student => student.IsActive)
                        // .Select(student => new StudentDto(student.Id, student.Name))
                        .OrderByDescending(student => student.CreatedAt)
                        .ToListAsync();

                    return Results.Ok(students);
                }
            );

            // PUT
            app.MapPut(
                "student/{id:guid}",
                async (Guid id, PutStudent request, AppDbContext context) =>
                {
                    var student = await context.Students
                        .SingleOrDefaultAsync(student => student.Id == id);

                    if (student == null) return Results.NotFound();

                    student.UpdateStudentName(request.Name);

                    await context.SaveChangesAsync();

                    return Results.Ok();
                }
            );

            // DELETE
            app.MapDelete(
                "student/{id:guid}",
                async (Guid id, AppDbContext context) =>
                {
                    var student = await context.Students
                        .SingleOrDefaultAsync(student => student.Id == id);

                    if (student == null) return Results.NotFound();

                    student.InactivateStudent();

                    await context.SaveChangesAsync();

                    return Results.Ok();
                }
            );
        }
    }
}