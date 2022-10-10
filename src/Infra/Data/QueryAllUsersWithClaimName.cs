using AppStore.Endpoints.Employees.Dto;
using Dapper;
using Microsoft.Data.SqlClient;

namespace AppStore.Infra.Data;

public class QueryAllUsersWithClaimName
{
    private readonly IConfiguration configuration;

    public QueryAllUsersWithClaimName(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public async Task<IEnumerable<EmployeeResponse>> Execute(int page, int rows) 
    {
        var db = new SqlConnection(configuration["ConnectionString:AppStoreDb"]);
        var query =
            @"SELECT u.Email, c.ClaimValue AS Name
                FROM AspNetUsers AS u INNER JOIN AspNetUserClaims AS c 
                ON c.UserId = u.Id AND c.claimtype = 'Name' 
            ORDER BY Name
            OFFSET (@page - 1) * @rows ROWS FETCH NEXT @rows ROWS ONLY";

        return await db.QueryAsync<EmployeeResponse>(
            query,
            new { page, rows }
        );
    }

}
