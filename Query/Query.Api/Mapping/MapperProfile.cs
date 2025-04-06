using AutoMapper;
using Query.Api.Queries.Client;
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
    }
}
