using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Query.Api.Queries.Client;
using Query.Infrastructure;

namespace Query.Api.QueryHandlers;

public class ClientQueryHandler(DataContext context, IMapper mapper)
    : IRequestHandler<ListAllClientsRequest, ListAllClientsResponse>
    , IRequestHandler<GetClientByIdRequest, GetClientByIdResponse>
{
    private readonly DataContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<ListAllClientsResponse> Handle(
        ListAllClientsRequest request, CancellationToken cancellationToken)
    {
        var clients = await _context.Clients
            .Where(client => string.IsNullOrEmpty(request.NameContains) || client.FullName.Contains(request.NameContains))
            .Where(client => string.IsNullOrEmpty(request.AddressContains) || client.FullAddress.Contains(request.AddressContains))
            .Where(client => !request.Status.HasValue || client.Status == request.Status)
            .ToListAsync(cancellationToken);

        var clientDtos = clients.Select(_mapper.Map<ListClientDto>);

        return new ListAllClientsResponse { Clients = clientDtos };
    }

    public async Task<GetClientByIdResponse> Handle(
        GetClientByIdRequest request, CancellationToken cancellationToken)
    {
        var client = await _context.Clients
            .Include(client => client.Contacts.Where(contact => !contact.IsDeleted))
            .SingleOrDefaultAsync(client => client.Id == request.Id, cancellationToken);

        var clientDto = _mapper.Map<ClientDto>(client);

        return new GetClientByIdResponse { Client = clientDto };
    }
}
