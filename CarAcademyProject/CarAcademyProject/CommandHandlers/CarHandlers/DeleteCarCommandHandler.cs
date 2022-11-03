using CarAcademyProjectDL.RepoInterfaces;
using CarAcademyProjectModels.MediatR.CarCommands;
using CarAcademyProjectModels.Response;
using MediatR;

namespace CarAcademyProject.CommandHandlers.CarHandlers
{
    public class DeleteCarCommandHandler : IRequestHandler<DeleteCarCommand, AddCarResponse>
    {
        private readonly ICarRepository _carRepository;

        public DeleteCarCommandHandler(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<AddCarResponse> Handle(DeleteCarCommand request, CancellationToken cancellationToken)
        {
            if (request == null) return new AddCarResponse() { HttpStatusCode = System.Net.HttpStatusCode.BadRequest, Message = "The request is empty" };

            var result = await _carRepository.GetCarByPlateNumber(request._platNumber);
            if (result == null) return new AddCarResponse() { HttpStatusCode = System.Net.HttpStatusCode.BadRequest, Message = "This car does not exists" };

            await _carRepository.DeleteCar(result.Id);
            return new AddCarResponse() { HttpStatusCode = System.Net.HttpStatusCode.OK, Car = result };
        }
    }
}
