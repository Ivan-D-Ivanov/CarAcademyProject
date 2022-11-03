using System.Data.SqlClient;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace CarAcademyProject.HealthChecks
{
    public class SqlHealthCheck : IHealthCheck
    {
        private readonly IConfiguration _configuration;

        public SqlHealthCheck(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                try
                {
                    await conn.OpenAsync(cancellationToken);
                }
                catch (Exception e)
                {
                    return HealthCheckResult.Unhealthy(e.Message);
                }

                return HealthCheckResult.Healthy("Sql Connection is OK");
            }
        }
    }
}
