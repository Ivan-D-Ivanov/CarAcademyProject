using CarAcademyProjectBL.DataFlowService;
using CarAcademyProjectBL.Services;
using CarAcademyProjectModels.ConfigurationM;
using CarAcademyProjectModels.MediatR.CarServiceCommands;
using CarAcademyProjectModels.Request;
using MediatR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CarAcademyProjectBL.BackGroundServices
{
    public class HighLevelServiceSubscriber : IHostedService
    {
        private readonly ConsumerService<int, PublishCarServiceRequest> _consumerService;
        private readonly ILogger<HighLevelServiceSubscriber> _logger;
        private readonly IOptionsMonitor<HighLevelCarConsumerSettings> _consumerSettings;
        private readonly HighLevelServiceDataFlow _dataFlow;

        public HighLevelServiceSubscriber(
            IOptionsMonitor<HighLevelCarConsumerSettings> consumerSettings,
            ILogger<HighLevelServiceSubscriber> logger, IMediator mediator,
            HighLevelServiceDataFlow dataFlow)
        {
            _logger = logger;
            _consumerSettings = consumerSettings;
            _consumerService = new ConsumerService<int, PublishCarServiceRequest>(_consumerSettings);
            _dataFlow = dataFlow;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Task.Run(() =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    var obj = _consumerService.Consume();
                    _logger.LogInformation($"The obj is {obj.Value.ClientName}, {obj.Value.CarPlateNumber}, {obj.Value.ManipulationDescription}, {obj.Value.MDifficult}");
                    _dataFlow.ProceedHighLevelService(obj.Value);
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
