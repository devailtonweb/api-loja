using AppStore.Endpoints.Categories.Dto;
using AppStore.Infra.Data;

namespace AppStore.Endpoints.Categories;

public class CategoryGetAll
{
    public static string Template => "/categories";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    public static IResult Action(ApplicationDbContext context) 
    {
        var category = context.Categories.ToList();
        if (category == null)
            return Results.NotFound();
        var response = category.Select(c => new CategoryResponse { Id = c.Id, Name = c.Name, Active = c.Active });

        return Results.Ok(response);
    }
}
