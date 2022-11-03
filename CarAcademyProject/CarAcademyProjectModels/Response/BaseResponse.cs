using System.Net;

namespace CarAcademyProjectModels.Response
{
    public class BaseResponse
    {
        public HttpStatusCode HttpStatusCode { get; init; }

        public string? Message { get; set; }
    }
}
