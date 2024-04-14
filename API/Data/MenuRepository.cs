using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Data
{
    public class MenuRepository : IMenuRepository
    {
        private readonly DataContext _context;

        public MenuRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Menu> CreateMenuItemAsync(Menu menu)
        {
            await _context.Menus.AddAsync(menu);
            return menu;
        }

        public void DeleteMenuItem(Menu menu)
        {
            _context.Menus.Remove(menu);
        }

        public async Task<IEnumerable<Menu>> GetMenuAsync()
        {
            return await _context.Menus.ToListAsync();
        }

        public async Task<Menu> GetMenuItemByIdAsync(int id)
        {
            return await _context.Menus.FindAsync(id);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

    }
}
