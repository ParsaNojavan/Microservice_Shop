using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Ordering.Application.Features.Orders.Queries.GetOrdersLists
{
    public class GetOrdersListQuery : IRequest<List<OrdersDto>>
    {
        public string UserName { get; set; }
        public GetOrdersListQuery(string userName)
        {
            UserName = userName;
        }
    }
}
