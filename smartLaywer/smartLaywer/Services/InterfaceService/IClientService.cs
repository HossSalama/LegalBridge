namespace smartLaywer.Services.InterfaceService
{
    public interface IClientService
    {
        /// <summary>
        /// Saves a new client, stamps timestamps, returns entity with generated Id.
        /// Called from AddCase form when user chooses "new client".
        /// </summary>
        Task<Client> AddClientAsync(Client client);

        /// <summary>
        /// Returns active clients as full entities for the AddCase dropdown.
        /// </summary>
        Task<List<Client>> GetActiveClientsDropdownAsync();

        /// <summary>
        /// Returns all clients projected to ClientSummaryDto for the Clients page table.
        /// </summary>
        Task<List<ClientSummaryDto>> GetAllClientsAsync();

        /// <summary>
        /// Returns a single client by Id as the full entity.
        /// Used to pre-populate the edit form.
        /// Returns null if not found.
        /// </summary>
        Task<Client?> GetClientByIdAsync(int id);

    

        /// <summary>
        /// Updates editable client fields. Does NOT change IsActive or CreatedAt.
        /// Throws KeyNotFoundException if client does not exist.
        /// </summary>
        Task UpdateClientAsync(Client client);
    }
}