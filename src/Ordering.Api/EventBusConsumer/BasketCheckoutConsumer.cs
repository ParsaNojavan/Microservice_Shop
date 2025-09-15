using AutoMapper;
using EventBus.Messages.Events;
using MassTransit;
using Ordering.Application.Features.Orders.Commands.CheckoutOrder;
using MediatR;

namespace Ordering.Api.EventBusConsumer
{
    public class BasketCheckoutConsumer : IConsumer<CheckoutEvent>
    {
        private readonly ILogger<BasketCheckoutConsumer> _logger;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public BasketCheckoutConsumer(IMapper mapper, IMediator mediator
            , ILogger<BasketCheckoutConsumer> logger)
        {
            _mediator = mediator;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<CheckoutEvent> context)
        {
            var command = _mapper.Map<CheckoutOrderCommand>(context.Message);
            await _mediator.Send(command);
            _logger.LogInformation("Order consumed successfully");
        }
    }
}
