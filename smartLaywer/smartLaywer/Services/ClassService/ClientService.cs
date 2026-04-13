using smartLaywer.DTO.Clients;
using smartLaywer.Repository.UnitWork;

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
        /// Called from AddCase form when user selects "new client".
        /// </summary>
        public async Task<Client> AddClientAsync(Client client)
        {
            client.CreatedAt = DateTime.Now;
            client.IsActive = true;
            await _unitOfWork.Client.AddAsync(client);   // IClientRepository - NOT Clients (generic)
            await _unitOfWork.CompleteAsync();
            return client; 
        }

        public async Task<List<ClientSummaryDto>> GetAllClientsAsync()
        {
            var clients = await _unitOfWork.Client.GetAllWithCasesAsync();

            return clients.Select(c => new ClientSummaryDto
            {
                Id = c.Id,
                FullName = c.FullName,
                Phone = c.Phone,
                SecondaryPhone = c.SecondaryPhone,
                Email = c.Email,
                Address = c.Address,
                ClientType = c.ClientType.GetDisplayName(),
                IsActive = c.IsActive,
                ActiveCasesCount = c.Cases.Count(ca => !ca.IsArchived),
                CreatedAt = c.CreatedAt
            }).ToList();
        }


        public async Task<Client?> GetClientByIdAsync(int id)
            => await _unitOfWork.Client.GetByIdAsync(id);


        public async Task<List<Client>> GetActiveClientsDropdownAsync()
        {
            return await _unitOfWork.Clients
                .GetAllQueryableNoTracking()
                .Where(c => c.IsActive)
                .OrderBy(c => c.FullName)
                .ToListAsync();
        }


        public async Task UpdateClientAsync(Client updated)
        {
            var existing = await _unitOfWork.Client.GetByIdAsync(updated.Id)
                ?? throw new KeyNotFoundException($"Client {updated.Id} not found.");

            existing.FullName = updated.FullName;
            existing.Phone = updated.Phone;
            existing.SecondaryPhone = updated.SecondaryPhone;
            existing.Email = updated.Email;
            existing.Address = updated.Address;
            existing.NationalId = updated.NationalId;
            existing.CommercialReg = updated.CommercialReg;
            existing.JobTitle = updated.JobTitle;
            existing.Gender = updated.Gender;
            existing.ClientType = updated.ClientType;
            existing.IsActive = updated.IsActive;

            _unitOfWork.Client.Update(existing);
            await _unitOfWork.CompleteAsync();
        }
    }
}