namespace Query.Api.Queries.Client;

public class ListAllClientsResponse
{
    public IEnumerable<ListClientDto> Clients { get; set; } = [];
}

public class GetClientByIdResponse
{
    public ClientDto Client { get; set; }
}
