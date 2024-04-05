using API.Entities;

namespace API.Interfaces
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetOrdersAsync();
        Task<Order> GetOrderByIdAsync(int id);
        Task<Order> CreateOrderAsync(Order order);
        void DeleteOrder(Order order);
        Task<bool> SaveAllAsync();
    }
}
