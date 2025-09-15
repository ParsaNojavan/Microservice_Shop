using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ordering.Domain.Entities;
using Ordering.Infrastructure.Persistance;
using Ordering.Application.Contracts.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Ordering.Infrastructure.Repositories
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(OrderContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserName(string userName)
        {
            return await _context.Orders
                .Where(o => o.UserName == userName)
                    .ToListAsync();
        }
    }
}
