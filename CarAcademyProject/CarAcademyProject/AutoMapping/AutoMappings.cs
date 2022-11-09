using AutoMapper;
using CarAcademyProjectModels;
using CarAcademyProjectModels.Request;
using CarAcademyProjectModels.Response;

namespace CarAcademyProject.AutoMapping
{
    public class AutoMappings : Profile
    {
        public AutoMappings()
        {
            CreateMap<AddCarRequest, Car>();
            CreateMap<ClientRequest, Client>();
            CreateMap<PublishCarServiceRequest, CarServiceRquest>();
            CreateMap<PublishCarServiceRequest, MongoCarService>();
            CreateMap<MongoCarService, CarServiceRquest>();
        }
    }
}
