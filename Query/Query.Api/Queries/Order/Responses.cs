namespace Query.Api.Queries.Order;

public class ListAllOrdersResponse
{
    public IEnumerable<OrderDto> Orders { get; set; }
}

public class GetOrderByIdResponse
{
    public OrderDto Order { get; set; }
}
