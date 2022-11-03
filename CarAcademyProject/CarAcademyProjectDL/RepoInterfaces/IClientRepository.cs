using CarAcademyProjectModels;

namespace CarAcademyProjectDL.RepoInterfaces
{
    public interface IClientRepository
    {
        Task<IEnumerable<Client>> GetAllClients();
        Task<Client?> GetClientById(int id);
        Task<Client?> GetClientByName(string name);
        Task<Client?> AddClient(Client client);
        Task<Client?> DeleteClient(int clientId);
        Task<Client?> UpdateClient(Client client);
    }
}
