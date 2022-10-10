using AppStore.Endpoints.Categories.Dto;
using AppStore.Infra.Data;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AppStore.Endpoints.Categories;

public class CategoryPut
{
    public static string Template => "/categories/{id:guid}";
    public static string[] Methods => new string[] { HttpMethod.Put.ToString() };
    public static Delegate Handle => Action;

    public static IResult Action(
        [FromRoute] Guid id, 
        EmployeeRequest categoryRequest, 
        ApplicationDbContext context,
        HttpContext http) 
    {
        var userId = http.User.Claims.First(categoryRequest => categoryRequest.Type == ClaimTypes.NameIdentifier).Value;
        var category = context.Categories.Where(c => c.Id == id).FirstOrDefault();
        if (category == null)
            return Results.NotFound();

        category.EditInfo(categoryRequest.Name, categoryRequest.Active, userId);

        if (!category.IsValid)
            return Results.ValidationProblem(category.Notifications.ConvertToProblemDetails());

        context.SaveChanges();

        return Results.Ok();
    }
}
