using CarAcademyProjectBL.DataFlowService;
using CarAcademyProjectBL.Services;
using CarAcademyProjectModels.ConfigurationM;
using CarAcademyProjectModels.Request;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CarAcademyProjectBL.BackGroundServices
{
    public class KafkaCarServiceSubscriber : IHostedService
    {
        private readonly ConsumerService<int, PublishCarServiceRequest> _consumerService;
        private readonly ILogger<KafkaCarServiceSubscriber> _logger;
        private readonly CarServiceDataFlow _carServiceDataFlow;
        private readonly IOptionsMonitor<KafkaConsumerSettings> _consumerSettings;


        public KafkaCarServiceSubscriber(ILogger<KafkaCarServiceSubscriber> logger, CarServiceDataFlow carServiceDataFlow, IOptionsMonitor<KafkaConsumerSettings> consumerSettings)
        {
            _logger = logger;
            _carServiceDataFlow = carServiceDataFlow;
            _consumerSettings = consumerSettings;
            _consumerService = new ConsumerService<int, PublishCarServiceRequest>(_consumerSettings);
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
            return Task.CompletedTask;
        }
    }
}
