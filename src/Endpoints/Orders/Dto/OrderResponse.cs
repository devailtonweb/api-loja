namespace AppStore.Endpoints.Orders.Dto;

public record OrderResponse(
    Guid Id,
    string ClientEmail,
    IEnumerable<OrderProduct> Products, 
    string DeliveryAddress);

public record OrderProduct(Guid Id, String Name);