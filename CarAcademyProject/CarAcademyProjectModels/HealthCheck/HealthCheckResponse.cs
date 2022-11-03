namespace CarAcademyProjectModels.HealthChecks
{
    public class HealthCheckResponse
    {
        public string Status { get; init; }

        public TimeSpan HealthCheckDuration { get; init; }

        public IEnumerable<IndividualHealthCheckResponse> HealthChecks { get; init; }
    }
}
