using API.Data;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace API.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DataContext _context;

        public OrderRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            return await _context.Orders.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Order> CreateOrderAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            return order;
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync()
        {
            return await _context.Orders.Include(x => x.User).ToListAsync();
        }

        public void DeleteOrder(Order order)
        {
            _context.Orders.Remove(order);
        }
        public async Task<bool> DeactiveOrderAsync(int id)
        {
            var item = await _context.Orders.FirstOrDefaultAsync(x => x.Id == id);
            item.IsActive = false;
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
