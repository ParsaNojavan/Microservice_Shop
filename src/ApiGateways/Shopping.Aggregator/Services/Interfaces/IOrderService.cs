using Shopping.Aggregator.Models;

namespace Shopping.Aggregator.Services.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderResponseModel>> GetOrderByUserName(string userName);
    }
}
