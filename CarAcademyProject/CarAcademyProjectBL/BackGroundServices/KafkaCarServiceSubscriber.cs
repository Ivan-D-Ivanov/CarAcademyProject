using CarAcademyProjectBL.DataFlowService;
using CarAcademyProjectBL.Services;
using CarAcademyProjectModels.Request;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CarAcademyProjectBL.BackGroundServices
{
    public class KafkaCarServiceSubscriber : IHostedService
    {
        private readonly ConsumerService<int, PublishCarServiceRequest> _consumerService;
        private readonly ILogger<KafkaCarServiceSubscriber> _logger;
        private readonly CarServiceDataFlow _carServiceDataFlow;


        public KafkaCarServiceSubscriber(ConsumerService<int,
            PublishCarServiceRequest> consumerService,
            ILogger<KafkaCarServiceSubscriber> logger,
            CarServiceDataFlow carServiceDataFlow)
        {
            _consumerService = consumerService;
            _logger = logger;
            _carServiceDataFlow = carServiceDataFlow;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Task.Run(() =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    var obj = _consumerService.Consume();
                    _logger.LogInformation($"The obj is {obj.Value.ClientName}, {obj.Value.CarPlateNumber}, {obj.Value.ManipulationDescription}, {obj.Value.MDifficult}");
                    _carServiceDataFlow.ProceedCarService(obj.Value);
                }
            });

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
