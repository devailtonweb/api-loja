namespace AppStore.Endpoints.Categories.Dto;

public class CategoryResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public bool Active { get; set; }
}