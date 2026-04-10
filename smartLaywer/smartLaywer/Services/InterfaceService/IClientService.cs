namespace smartLaywer.Services.InterfaceService
{
    public interface IClientService
    {
        Task<Client> AddClientAsync(Client client);
    }
}
