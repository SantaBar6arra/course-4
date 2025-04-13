namespace Query.Api.Queries.Product;

public class ListAllProductsResponse
{
    public IEnumerable<ListProductDto> Products { get; set; } = [];
}

public class GetProductByIdResponse
{
    public ProductDto Product { get; set; }
}
