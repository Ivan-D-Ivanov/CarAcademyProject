using System.Threading.Tasks.Dataflow;
using AutoMapper;
using CarAcademyProjectBL.HighLevelServices;
using CarAcademyProjectModels.Request;
using CarAcademyProjectModels.Response;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CarAcademyProjectBL.DataFlowService
{
    public class HighLevelServiceDataFlow
    {
        private readonly HighLevelRepoService _highLevelService;
        private readonly IMapper _mapper;
        private readonly ILogger<HighLevelServiceDataFlow> _logger;

        public HighLevelServiceDataFlow(IMediator mediator, ILogger<HighLevelServiceDataFlow> logger, IMapper mapper, HighLevelRepoService highLevelServiceDataFlow)
        {
            _logger = logger;
            _mapper = mapper;
            _highLevelService = highLevelServiceDataFlow;
        }

        public Task ProceedHighLevelService(PublishCarServiceRequest carService)
        {
            var transformBlock = new TransformBlock<PublishCarServiceRequest, MongoCarService>(cs =>
            {
                return _mapper.Map<MongoCarService>(cs);
            });

            var actionBlock = new ActionBlock<MongoCarService>(async b =>
            {
                b.Id = Guid.NewGuid().ToString();
                _logger.LogInformation($"The CarService is added in MongoDb {b.ClientName}, {b.CarPlateNumber}");
                await _highLevelService.SaveCarService(b);
            });

            transformBlock.LinkTo(actionBlock);
            transformBlock.Post(carService);
            return Task.CompletedTask;
        }
    }
}
