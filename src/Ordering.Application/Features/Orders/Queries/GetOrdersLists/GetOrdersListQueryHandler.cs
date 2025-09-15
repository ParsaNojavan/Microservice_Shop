using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Ordering.Application.Contracts.Persistance;

namespace Ordering.Application.Features.Orders.Queries.GetOrdersLists
{
    public class GetOrdersListQueryHandler : IRequestHandler<GetOrdersListQuery,List<OrdersDto>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        public GetOrdersListQueryHandler(IOrderRepository orderRepository
            , IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<List<OrdersDto>> Handle(GetOrdersListQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository
                .GetOrdersByUserName(request.UserName);
            return(_mapper.Map<List<OrdersDto>>(orders));
        }
    }
}
