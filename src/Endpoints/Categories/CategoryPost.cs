using AppStore.Domain.Products;
using AppStore.Endpoints.Categories.Dto;
using AppStore.Infra.Data;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace AppStore.Endpoints.Categories;

public class CategoryPost
{
    public static string Template => "/categories";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => Action;

    [Authorize(Policy = "EmployeePolicy")]
    public static async Task<IResult> Action(
        EmployeeRequest categoryRequest, 
        HttpContext http,
        ApplicationDbContext context) 
    {

        /*if (string.IsNullOrEmpty(categoryRequest.Name))
            return Results.BadRequest("Name is required");*/
        var userId = http.User.Claims.First(categoryRequest => categoryRequest.Type == ClaimTypes.NameIdentifier).Value;
        var category = new Category(categoryRequest.Name, userId, userId);

        if (!category.IsValid)
            return Results.ValidationProblem(category.Notifications.ConvertToProblemDetails());

        context.Categories.AddAsync(category);
        context.SaveChangesAsync();

        return Results.Created($"/categories/{category.Id}", category.Id);
    }
}
