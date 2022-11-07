using System.Threading.Tasks.Dataflow;
using AutoMapper;
using CarAcademyProject.CarAcademyProjectBL.CarPublishService;
using CarAcademyProjectModels.ConfigurationM;
using CarAcademyProjectModels.MediatR.CarServiceCommands;
using CarAcademyProjectModels.Request;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CarAcademyProjectBL.DataFlowService
{
    public class CarServiceDataFlow
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<CarServiceDataFlow> _logger;
        private readonly IOptionsMonitor<HighLevelCarPublisherSettings> _kafkaSettings;
        private readonly IKafkaPublisherService<int, PublishCarServiceRequest> _kafkaPublisherService;

        public CarServiceDataFlow(IMediator mediator,
            IMapper mapper,
            ILogger<CarServiceDataFlow> logger,
            IOptionsMonitor<HighLevelCarPublisherSettings> kafkaSettings)
        {
            _mediator = mediator;
            _mapper = mapper;
            _logger = logger;
            _kafkaSettings = kafkaSettings;
            _kafkaPublisherService = new KafkaPublisherService<int, PublishCarServiceRequest>(_kafkaSettings);
        }

        public Task ProceedCarService(PublishCarServiceRequest carServiceRequest)
        {
            var transformBlock = new TransformBlock<PublishCarServiceRequest, CarServiceRquest>(cs =>
            {
                var carService = _mapper.Map<CarServiceRquest>(cs);
                return carService;
            });

            var actionBlock = new ActionBlock<CarServiceRquest>(async b =>
            {
                var difficulty = (int)b.MDifficult;
                if (difficulty > 2)
                {
                    _logger.LogInformation($"The Car Manipulation Difficulty is high, the {b.ManipulationDescription} is going to be finished after some days!");
                    await _kafkaPublisherService.PublishTopic(difficulty, carServiceRequest);
                }
                else
                {
                    var result = await _mediator.Send(new AddCarServiceCommand(b));
                    result.CarService.EndDate = DateTime.UtcNow;
                    _logger.LogInformation($"Successfully added CarService : {result.CarService.ClientId}, {result.CarService.StartDate} - {result.CarService.EndDate}");
                }
            });

            transformBlock.LinkTo(actionBlock);

            transformBlock.Post(carServiceRequest);
            return Task.CompletedTask;
        }
    }
}
