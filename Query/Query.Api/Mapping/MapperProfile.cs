using AutoMapper;
using Query.Api.Queries.Client;
using Query.Api.Queries.Product;
using Query.Domain.Entities;

namespace Query.Api.Mapping;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Client, ListClientDto>()
            .ForMember(d => d.Address, opt => opt.MapFrom(s => s.FullAddress));
        CreateMap<Client, ClientDto>();
        CreateMap<ClientContact, ClientContactDto>();

        CreateMap<Product, ListProductDto>();
        CreateMap<Product, ProductDto>()
            .ForMember(d => d.Tags, opt => opt.MapFrom(s => s.Tags.Select(tag => tag.Name)));
    }
}
