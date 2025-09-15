using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Ordering.Domain.Entities;

namespace Ordering.Infrastructure.Persistance
{
    public class OrderSeed
    {
        public static async Task SeedAsync(OrderContext orderContext, ILogger<OrderContext> logger)
        {
            if (!await orderContext.Orders.AnyAsync())
            {
                await orderContext.Orders.AddRangeAsync(GetPreconfiguredOrders());
                await orderContext.SaveChangesAsync();
                logger.LogInformation("Data seed section configured");
            }
        }

        public static IEnumerable<Order> GetPreconfiguredOrders()
        {
            return new List<Order>
            {
                new Order
                {
                    FirstName = "Parsa",
                    LastName = "Nojavan",
                    UserName = "ParsaNojavan",
                    EmailAdress = "parsa.nojavan85@gmail.com",
                    City = "Tabriz",
                    Country = "Iran",
                    TotalPrice = 10000,
                    BankName = "Mellat",
                    ZipCode = "5443123",
                    RefCode = "1234-5678-9876-5432",
                    PaymentMethod = 1
                }
            };
        }
    }
}
