using Microsoft.EntityFrameworkCore;
using PersonAPI.Data;
using PersonAPI.Models;

namespace PersonAPI.Routes;

public static class PersonRoute
{
    public static void PersonRoutes(this WebApplication app)
    {
        var personRoute = app.MapGroup("/person");

        personRoute.MapPost("/",
        async (PersonRequest request, PersonContext context) =>
        {
            var person = new PersonModel(request.Name);
            await context.AddAsync(person);
            await context.SaveChangesAsync();

            return Results.Created();
        });

        personRoute.MapGet("/",
        async (PersonContext context) =>
        {
            var people = await context.People.ToListAsync();
            return Results.Ok(people);
        });

        personRoute.MapGet("/{id:guid}",
        async (Guid id, PersonContext context) =>
        {
            var person = await context.People.FirstOrDefaultAsync(x => x.Id == id);

            if (person == null)
            {
                return Results.NotFound();
            }

            return Results.Ok(person);
        });

        personRoute.MapPut("/{id:guid}",
        async (Guid id, PersonRequest request, PersonContext context) =>
        {
            var person = await context.People.FirstOrDefaultAsync(x => x.Id == id);

            if (person == null)
            {
                return Results.NotFound();
            }

            person.ChangeName(request.Name);
            await context.SaveChangesAsync();

            return Results.Ok(person);
        });

        personRoute.MapDelete("/{id:guid}",
        async (Guid id, PersonContext context) =>
        {
            var person = await context.People.FirstOrDefaultAsync(x => x.Id == id);

            if (person == null)
            {
                return Results.NotFound();
            }

            context.People.Remove(person);
            await context.SaveChangesAsync();

            return Results.NoContent();
        });
    }
}
