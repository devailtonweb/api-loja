using AppStore.Endpoints.Products.Dto;

namespace AppStore.Infra.Data;

public class QueryAllProductsSold
{

    private readonly IConfiguration configuration;

    public QueryAllProductsSold(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public async Task<IEnumerable<ProductSoldResponse>> Execute()
    {
        var db = new SqlConnection(configuration["ConnectionString:AppStoreDb"]);
        var query = @"SELECT
                        p.ID,
                        p.Name,
                        count(*) Amount
                      FROM
                        Orders AS o INNER JOIN OrderProducts AS OP
                            ON o.Id = op.OrdersId
                        INNER JOIN Products AS p 
                            ON p.Id = op.ProductsId
                      GROUP BY
                        p.Id, p.Name
                      ORDER BY Amount DESC
                    ";

        return await db.QueryAsync<ProductSoldResponse>(query);
    }

}
