using CarAcademyProjectDL.RepoInterfaces;
using CarAcademyProjectModels;
using CarAcademyProjectModels.MediatR.CarCommands;
using MediatR;

namespace CarAcademyProject.CommandHandlers.CarHandlers
{
    public class GetAllCarsCommandHandler : IRequestHandler<GetAllCarsCommand, IEnumerable<Car>>
    {
        private readonly ICarRepository _carRepository;

        public GetAllCarsCommandHandler(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<IEnumerable<Car>> Handle(GetAllCarsCommand request, CancellationToken cancellationToken)
        {
            return await _carRepository.GetAllCars();
        }
    }
}
