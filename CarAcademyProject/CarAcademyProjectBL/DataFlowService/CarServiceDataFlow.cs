using System.Threading.Tasks.Dataflow;
using AutoMapper;
using CarAcademyProjectModels.MediatR.CarServiceCommands;
using CarAcademyProjectModels.Request;
using MediatR;

namespace CarAcademyProjectBL.DataFlowService
{
    public class CarServiceDataFlow
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CarServiceDataFlow(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
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
            });

            bufferBlock.LinkTo(transformBlock);
            transformBlock.LinkTo(actionBlock);

            bufferBlock.Post(carServiceRequest);
            return Task.CompletedTask;
        }
    }
}
