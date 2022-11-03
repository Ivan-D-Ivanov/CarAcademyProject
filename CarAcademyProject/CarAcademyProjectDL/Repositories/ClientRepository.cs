using System.Data.SqlClient;
using CarAcademyProjectDL.RepoInterfaces;
using CarAcademyProjectModels;
using CarAcademyProjectModels.ConfigurationM;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CarAcademyProjectDL.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly ILogger<ClientRepository> _logger;
        private readonly IOptionsMonitor<ConnectionStrings> _connectionString;

        public ClientRepository(ILogger<ClientRepository> logger, IConfiguration configuration, IOptionsMonitor<ConnectionStrings> connectionString)
        {
            _logger = logger;
            _connectionString = connectionString;
        }


        public async Task<Client?> AddClient(Client clinet)
        {
            try
            {
                await using (var connection = new SqlConnection(_connectionString.CurrentValue.DefaultConnection))
                {
                    await connection.OpenAsync();

                    return (await connection
                        .QueryAsync<Client>
                        ($"INSERT INTO Clients (FirstName,LastName,Age,CarId) VALUES(@FirstName,@LastName,@Age,@CarId)", clinet))
                        .SingleOrDefault();
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error from {nameof(AddClient)} : {e}");
            }

            return null;
        }

        public Task<Client?> DeleteClient(int clientId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Client>> GetAllClients()
        {
            try
            {
                await using (var connection = new SqlConnection(_connectionString.CurrentValue.DefaultConnection))
                {
                    var query = $"SELECT * FROM Clients WITH(NOLOCK)";
                    await connection.OpenAsync();

                    var clients = await connection.QueryAsync<Client>(query);
                    return clients;
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(GetAllClients)} : {e}");
            }

            return Enumerable.Empty<Client>();
        }

        public Task<Client?> GetClientById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Client?> GetClientByName(string name)
        {
            try
            {
                await using (var connection = new SqlConnection(_connectionString.CurrentValue.DefaultConnection))
                {
                    await connection.OpenAsync();

                    var client = await connection.QueryFirstOrDefaultAsync<Client>($"SELECT * FROM Clients WITH(NOLOCK) WHERE FirstName + ' ' + LastName LIKE @Name", new { Name = name });
                    return client;
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error from {nameof(GetClientByName)} : {e}");
            }

            return null;
        }

        public async Task<Client?> UpdateClient(Client client)
        {
            try
            {
                await using (var connection = new SqlConnection(_connectionString.CurrentValue.DefaultConnection))
                {
                    await connection.OpenAsync();

                    return (await connection
                        .QueryAsync<Client>
                        ($"UPDATE Clients SET FirstName=@FirstName,LastName=@LastName,Age=@Age,CarID=@CarID WHERE Id = @Id", client))
                        .SingleOrDefault();
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error from {nameof(UpdateClient)} : {e}");
            }

            return null;
        }
    }
}
