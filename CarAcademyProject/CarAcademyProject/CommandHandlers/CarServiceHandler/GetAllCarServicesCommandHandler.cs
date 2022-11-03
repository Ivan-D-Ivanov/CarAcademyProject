using CarAcademyProjectDL.RepoInterfaces;
using CarAcademyProjectModels;
using CarAcademyProjectModels.MediatR.CarServiceCommands;
using MediatR;

namespace CarAcademyProject.CommandHandlers.CarServiceHandler
{
    public class GetAllCarServicesCommandHandler : IRequestHandler<GetAllCarServiceCommand, IEnumerable<CarService>>
    {
        private readonly ICarServiceRepository _carServiceRepository;

        public GetAllCarServicesCommandHandler(ICarServiceRepository carServiceRepository)
        {
            _carServiceRepository = carServiceRepository;
        }

        public async Task<IEnumerable<CarService>> Handle(GetAllCarServiceCommand request, CancellationToken cancellationToken)
        {
            return await _carServiceRepository.GetAllCarServices();
        }
    }
}
