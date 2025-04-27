using Common.Models;
using MediatR;

namespace Query.Api.Queries.Order;

public class ListAllOrdersRequest : IRequest<ListAllOrdersResponse>
{
    public Guid? CustomerId { get; set; }
    public OrderStatus? Status { get; set; }
}

public class GetOrderByIdRequest : IRequest<GetOrderByIdResponse>
{
    public Guid Id { get; set; }
}
