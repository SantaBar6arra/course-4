using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Query.Api.Queries.Product;
using Query.Infrastructure;

namespace Query.Api.QueryHandlers;

public class ProductQueryHandler(DataContext context, IMapper mapper)
    : IRequestHandler<ListAllProductsRequest, ListAllProductsResponse>
    , IRequestHandler<GetProductByIdRequest, GetProductByIdResponse>
{
    public async Task<ListAllProductsResponse> Handle(
        ListAllProductsRequest request, CancellationToken cancellationToken)
    {
        var products = await context.Products
            .Include(product => product.Tags)
            .Where(product => string.IsNullOrEmpty(request.Name) || product.Name.Contains(request.Name, StringComparison.CurrentCultureIgnoreCase))
            .Where(product => request.PriceMin.HasValue || product.Price >= request.PriceMin)
            .Where(product => request.PriceMax.HasValue || product.Price <= request.PriceMax)
            .Where(product => string.IsNullOrEmpty(request.Category) || product.Category == request.Category)
            .Where(product => request.Tags == null || request.Tags.All(tag => product.Tags.Any(pTag => pTag.Name == tag)))
            .ToListAsync(cancellationToken);

        var productDtos = products.Select(mapper.Map<ListProductDto>);

        return new ListAllProductsResponse { Products = productDtos };
    }

    public async Task<GetProductByIdResponse> Handle(
        GetProductByIdRequest request, CancellationToken cancellationToken)
    {
        var product = await context.Products
            .Include(client => client.Tags)
            .SingleOrDefaultAsync(client => client.Id == request.Id, cancellationToken);

        var productDto = mapper.Map<ProductDto>(product);

        return new GetProductByIdResponse { Product = productDto };
    }
}
