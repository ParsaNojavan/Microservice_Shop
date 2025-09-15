using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Infrastructure;
using Ordering.Application.Contracts.Persistance;
using Ordering.Domain.Entities;

namespace Ordering.Application.Features.Orders.Commands.CheckoutOrder
{
    public class CheckoutOrderCommandHandler : IRequestHandler<CheckoutOrderCommand, int>
    {
        public IOrderRepository _orderRespository { get; }
        public IMapper _mapper { get; }
        public IEmailService _emailService { get; set; }
        public ILogger<CheckoutOrderCommandHandler> _logger { get; set; }
        public CheckoutOrderCommandHandler(IMapper mapper,IOrderRepository orderRepository
            ,ILogger<CheckoutOrderCommandHandler> logger,IEmailService emailService)
        {
            _orderRespository = orderRepository;
            _emailService = emailService;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<int> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
        {
            var orderEntity = _mapper.Map<Order>(request);
            var newOrder = await _orderRespository.AddAsync(orderEntity);
            _logger.LogInformation($"order {newOrder.Id} is successfully created");
            return newOrder.Id;
        }
    }
}
