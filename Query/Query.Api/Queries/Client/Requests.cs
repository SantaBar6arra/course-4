using Common.Models;
using MediatR;

namespace Query.Api.Queries.Client;

public class ListAllClientsRequest : IRequest<ListAllClientsResponse>
{
    public string? NameContains { get; set; }
    public string? AddressContains { get; set; }
    public ClientStatus? Status { get; set; }
}

public class GetClientByIdRequest(Guid id) : IRequest<GetClientByIdResponse>
{
    public Guid Id { get; set; } = id;
}
