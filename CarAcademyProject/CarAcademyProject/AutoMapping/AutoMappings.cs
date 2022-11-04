using AutoMapper;
using CarAcademyProjectModels;
using CarAcademyProjectModels.Request;

namespace CarAcademyProject.AutoMapping
{
    internal class AutoMappings : Profile
    {
        public AutoMappings()
        {
            CreateMap<AddCarRequest, Car>();
            CreateMap<ClientRequest, Client>();
            CreateMap<PublishCarServiceRequest, CarServiceRquest>();
        }
    }
}
