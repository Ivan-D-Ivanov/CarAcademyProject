using AutoMapper;
using CarAcademyProjectDL.RepoInterfaces;
using CarAcademyProjectModels;
using CarAcademyProjectModels.MediatR.CarCommands;
using CarAcademyProjectModels.Response;
using MediatR;

namespace CarAcademyProject.CommandHandlers.CarHandlers
{
    public class UpdateCarCommandHandler : IRequestHandler<UpdateCarCommand, AddCarResponse>
    {
        private readonly ICarRepository _carRepository;
        private readonly IMapper _mapper;

        public UpdateCarCommandHandler(ICarRepository carRepository, IMapper mapper)
        {
            _carRepository = carRepository;
            _mapper = mapper;
        }

        public async Task<AddCarResponse> Handle(UpdateCarCommand request, CancellationToken cancellationToken)
        {
            if (request._car == null) return new AddCarResponse() { HttpStatusCode = System.Net.HttpStatusCode.BadRequest, Message = "The request is null" };

            var result = await _carRepository.GetCarByPlateNumber(request._car.PlateNumber);
            if(result == null) return new AddCarResponse() { HttpStatusCode = System.Net.HttpStatusCode.BadRequest, Message = "There is no such Car" };

            var resultFromMap = _mapper.Map<Car>(request._car);
            resultFromMap.Id = result.Id;
            resultFromMap.LastUpdated = DateTime.UtcNow;
            await _carRepository.UpdateCar(resultFromMap);
            return new AddCarResponse() { HttpStatusCode = System.Net.HttpStatusCode.OK, Car = resultFromMap };
        }
    }
}
