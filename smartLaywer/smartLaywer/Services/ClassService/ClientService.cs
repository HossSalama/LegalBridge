namespace smartLaywer.Services.ClassService
{
    public class ClientService : IClientService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ClientService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Saves a new client and returns the entity with its generated Id.
        /// </summary>
        public async Task<Client> AddClientAsync(Client client)
        {
            client.CreatedAt = DateTime.Now;
            client.IsActive  = true;
            await _unitOfWork.Clients.AddAsync(client);
            await _unitOfWork.CompleteAsync();
            return client; // EF populates Id after SaveChanges
        }
    }
}
