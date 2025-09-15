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

namespace Ordering.Application.Features.Orders.Commands.DeleteOrder
{
    public class IDeleteOrderCommandHandler : IRequestHandler<IDeleteOrderCommand>
    {
        private IOrderRepository _orderRepository { get; }
        private ILogger<IDeleteOrderCommand> _logger { get; }
        private IMapper _mapper { get; }
        public IDeleteOrderCommandHandler(IOrderRepository orderRepository
            , ILogger<IDeleteOrderCommand> logger, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(IDeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var orderForDelete = await _orderRepository.GetByIdAsync(request.Id);

            if (orderForDelete == null)
            {
                _logger.LogError("order not exists");
                throw new NotFoundException(nameof(Order), request.Id);
            }
            else
            {
                await _orderRepository.DeleteAsync(orderForDelete);
                _logger.LogInformation($"order {orderForDelete.Id} is successfully deleted");
            }

            return Unit.Value;
        }
    }
}
