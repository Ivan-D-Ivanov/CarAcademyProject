using System.Threading.Tasks.Dataflow;
using AutoMapper;
using CarAcademyProject.CarAcademyProjectBL.CarPublishService;
using CarAcademyProjectModels.MediatR.CarServiceCommands;
using CarAcademyProjectModels.Request;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CarAcademyProjectBL.DataFlowService
{
    public class CarServiceDataFlow
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<CarServiceDataFlow> _logger;
        private readonly IKafkaPublisherService<int, PublishCarServiceRequest> _kafkaPublisherService;

        public CarServiceDataFlow(IMediator mediator,
            IMapper mapper,
            ILogger<CarServiceDataFlow> logger,
            IKafkaPublisherService<int, PublishCarServiceRequest> kafkaPublisherService)
        {
            _mediator = mediator;
            _mapper = mapper;
            _logger = logger;
            _kafkaPublisherService = kafkaPublisherService;
        }

        public Task ProceedCarService(PublishCarServiceRequest carServiceRequest)
        {
            var bufferBlock = new BufferBlock<PublishCarServiceRequest>();
            var transformBlock = new TransformBlock<PublishCarServiceRequest, CarServiceRquest>(cs =>
            {
                var difficulty = (int)cs.MDifficult;
                if (difficulty > 1)
                {
                    //TODO
                    return null;
                }
                else
                {
                    var carService = _mapper.Map<CarServiceRquest>(cs);
                    return carService;
                }
            });

            var actionBlock = new ActionBlock<CarServiceRquest>(async b =>
            {
                var result = await _mediator.Send(new AddCarServiceCommand(b));
                result.CarService.EndDate = DateTime.UtcNow;
                _logger.LogInformation($"Successfully added CarService : {result.CarService.ClientId}, {result.CarService.StartDate} - {result.CarService.EndDate}");
            });

            bufferBlock.LinkTo(transformBlock);
            transformBlock.LinkTo(actionBlock);

            bufferBlock.Post(carServiceRequest);
            return Task.CompletedTask;
        }
    }
}
