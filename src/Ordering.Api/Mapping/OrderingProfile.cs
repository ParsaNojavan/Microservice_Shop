using AutoMapper;
using EventBus.Messages.Events;
using Ordering.Api.EventBusConsumer;
using Ordering.Application.Features.Orders.Commands.CheckoutOrder;

namespace Ordering.Api.Mapping
{
    public class OrderingProfile : Profile
    {
        public OrderingProfile()
        {
            CreateMap<CheckoutOrderCommand,CheckoutEvent>().ReverseMap();
        }
    }
}
