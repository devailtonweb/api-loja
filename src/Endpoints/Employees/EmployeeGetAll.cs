using AppStore.Infra.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppStore.Endpoints.Employees;

public class EmployeeGetAll
{
    public static string Template => "/employees";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    [Authorize(Policy = "EmployeePolicy")]
    public static async Task<IResult> Action([FromQuery] int? page, [FromQuery] int? rows, QueryAllUsersWithClaimName query)
    {

        if (page is null || rows is null) 
            Results.NotFound("Nenhum registro encontrado");

        var result = await query.Execute(page.Value, rows.Value);
        return Results.Ok(result);
    }

    /*
     * Se a lista de usuários crescer teremos problemas com performance, iremos utilizar o Dapper par essa consulta
     * 
    public static IResult Action([FromQuery] int page, [FromQuery] int rows, UserManager<IdentityUser> userManager) 
    {
        var users = userManager.Users.Skip((page - 1) * rows).Take(rows).ToList();
        if (users == null)
            return Results.NotFound("Nenhum registro encontrado");

        var employees = new List<EmployeeResponse>();
        foreach (var item in users)
        {
            var claims = userManager.GetClaimsAsync(item).Result;
            var claimName = claims.FirstOrDefault(c => c.Type == "Name");
            var userName = claimName != null ? claimName.Value : string.Empty;
            employees.Add(new EmployeeResponse { Email = item.Email, Name = userName });
        }

        return Results.Ok(employees);
    }*/

}
