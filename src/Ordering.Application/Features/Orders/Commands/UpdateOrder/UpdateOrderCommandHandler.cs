using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Persistance;
using Ordering.Application.Exceptions;
using Ordering.Domain.Entities;

namespace Ordering.Application.Features.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand>
    {
        public IOrderRepository _orderRepository { get; }
        public IMapper _mapper { get; }
        public ILogger<UpdateOrderCommandHandler> _logger { get; }
        public UpdateOrderCommandHandler(IOrderRepository orderRepository
            , IMapper mapper, ILogger<UpdateOrderCommandHandler> logger)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var orderForUpdate = await _orderRepository.GetByIdAsync(request.Id);
            if (orderForUpdate == null)
            {
                _logger.LogError("order is not exists");
                throw new NotFoundException(nameof(Order), request.Id);
            }

            _mapper.Map(request, orderForUpdate, typeof(UpdateOrderCommand), typeof(Order));

            await _orderRepository.UpdateAsync(orderForUpdate);
            _logger.LogInformation($"order {orderForUpdate.Id} is successfully updated");

            return Unit.Value;
        }
    }
}
