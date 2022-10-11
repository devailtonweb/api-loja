using AppStore.Domain.Users;
using AppStore.Endpoints.Employees.Dto;

namespace AppStore.Endpoints.Employees;

public class EmployeePost
{
    public static string Template => "/employees";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => Action;

    [Authorize(Policy = "EmployeePolicy")]
    public static async Task<IResult> Action(
        EmployeeRequest employeeRequest, 
        HttpContext http,
        UserCreator userCreator) 
    {

        var userId = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

        var userClaims = new List<Claim>
        {
            new Claim("EmployeeCode", employeeRequest.EmployeeCode),
            new Claim("Name", employeeRequest.Name),
            new Claim("CreatedBy", userId)
        };

        (IdentityResult identity, string userId) result =
            await userCreator.Create(employeeRequest.Email, employeeRequest.Password, userClaims);

        if (!result.identity.Succeeded)
            return Results.ValidationProblem(result.identity.Errors.ConvertToProblemDetails());

        return Results.Created($"/employeee/{result.userId}", result.userId);

        /* var userId = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
         var user = new IdentityUser { UserName = employeeRequest.Email, Email = employeeRequest.Email };
         var result = await userManager.CreateAsync(user, employeeRequest.Password);

         if (!result.Succeeded)
             return Results.ValidationProblem(result.Errors.ConvertToProblemDetails());

         var userClaims = new List<Claim>
         {
             new Claim("EmployeeCode", employeeRequest.EmployeeCode),
             new Claim("Name", employeeRequest.Name),
             new Claim("CreatedBy", userId)
         };

         var claimResult = await userManager.AddClaimsAsync(user, userClaims);

         if (!claimResult.Succeeded)
             return Results.BadRequest(claimResult.Errors.First());

         return Results.Created($"/employeee/{user.Id}", user.Id);*/
    }
}
