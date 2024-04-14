using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Data
{
    public class ItemRepository : IItemRepository
    {
        private readonly DataContext _context;

        public ItemRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Item> CreateItemAsync(Item item)
        {
            await _context.Items.AddAsync(item);
            return item;
        }

        public void DeleteItem(Item item)
        {
            _context.Items.Remove(item);
        }

        public async Task<Item> GetItemByIdAsync(int id)
        {
            return await _context.Items.FindAsync(id);
        }
        public async Task<Item> GetItemByMenuIdAsync(int id)
        {
            return await _context.Items.FirstOrDefaultAsync(x => x.MenuId == id);
        }
        public async Task<IEnumerable<Item>> GetItemsByOrderIdAsync(int id)
        {
            return await _context.Items.Include("Order").Where(x => x.Order.Id == id).ToListAsync();
        }
        public async Task<bool> PlusItemAsync(OrderMenuIdDto om)
        {
            
            var item = await _context.Items.Include("Order").FirstOrDefaultAsync(x => x.Order.Id == om.OrderId && x.MenuId == om.MenuId);
            item.Quantity += 1;
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<bool> MinusItemAsync(OrderMenuIdDto om)
        {
            var item = await _context.Items.Include("Order").FirstOrDefaultAsync(x => x.Order.Id == om.OrderId && x.MenuId == om.MenuId);
            item.Quantity -= 1;
            if (item.Quantity == 0)
            {
                _context.Items.Remove(item);
            }    

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Item>> GetItemsAsync()
        {
            return await _context.Items.Include(x => x.Order).ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<bool> IsExist(int id)
        {
            return await _context.Items.AnyAsync(x => x.MenuId == id) ;
        }
        public async Task<bool> IsExistOrder(int id)
        {
            return await _context.Items.Include("Order").AnyAsync(x => x.Order.Id == id);
        }
    }
}
