using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Query.Api.Queries.Order;
using Query.Infrastructure;

namespace Query.Api.QueryHandlers;

public class OrderQueryHandler(DataContext context, IMapper mapper)
        : IRequestHandler<ListAllOrdersRequest, ListAllOrdersResponse>
        , IRequestHandler<GetOrderByIdRequest, GetOrderByIdResponse>
{
    public async Task<ListAllOrdersResponse> Handle(ListAllOrdersRequest request, CancellationToken cancellationToken)
    {
        var ordersQuery = context.Orders.AsQueryable();

        if (request.CustomerId.HasValue)
        {
            ordersQuery = ordersQuery.Where(order => order.CustomerId == request.CustomerId.Value);
        }

        if (request.Status.HasValue)
        {
            ordersQuery = ordersQuery.Where(order => order.Status == request.Status.Value);
        }

        var orders = await ordersQuery
            .Include(order => order.Items)
            .ToListAsync(cancellationToken);

        var orderDtos = orders.Select(mapper.Map<OrderDto>);

        return new ListAllOrdersResponse { Orders = orderDtos };
    }

    public async Task<GetOrderByIdResponse> Handle(GetOrderByIdRequest request, CancellationToken cancellationToken)
    {
        var order = await context.Orders
            .Include(order => order.Items)
            .SingleOrDefaultAsync(order => order.Id == request.Id, cancellationToken);

        if (order == null)
        {
            throw new KeyNotFoundException("Order not found!");
        }

        var orderDto = mapper.Map<OrderDto>(order);

        return new GetOrderByIdResponse { Order = orderDto };
    }
}
