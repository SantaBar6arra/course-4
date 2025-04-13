using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;

namespace Query.Api.Queries.Product;

public class ListAllProductsRequest : IRequest<ListAllProductsResponse>
{
    public string? Name { get; set; }
    public decimal? PriceMin { get; set; }
    public decimal? PriceMax { get; set; }
    public string? Category { get; set; }
    public IList<string>? Tags { get; set; }
};

public class GetProductByIdRequest(Guid id) : IRequest<GetProductByIdResponse>
{
    public Guid Id { get; set; } = id;
}
