using AutoMapper;
using CarAcademyProjectDL.RepoInterfaces;
using CarAcademyProjectModels;
using CarAcademyProjectModels.MediatR.CarCommands;
using CarAcademyProjectModels.Response;
using MediatR;

namespace CarAcademyProject.CommandHandlers.CarHandlers
{
    public class AddCarCommandHandler : IRequestHandler<AddCarCommand, AddCarResponse>
    {
        private readonly ICarRepository _carRepo;
        private readonly IMapper _mapper;

        public AddCarCommandHandler(ICarRepository carRepo, IMapper mapper)
        {
            _carRepo = carRepo;
            _mapper = mapper;
        }

        public async Task<AddCarResponse> Handle(AddCarCommand request, CancellationToken cancellationToken)
        {
            if (request == null) return new AddCarResponse() { HttpStatusCode = System.Net.HttpStatusCode.BadRequest, Message = "The request is null" };

            var resultFromMap = _mapper.Map<Car>(request._car);
            resultFromMap.LastUpdated = DateTime.UtcNow;
            await _carRepo.AddCar(resultFromMap);

            return new AddCarResponse() { HttpStatusCode = System.Net.HttpStatusCode.OK, Car = resultFromMap };
        }
    }
}
