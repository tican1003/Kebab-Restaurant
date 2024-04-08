using API.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Interfaces
{
    public interface IMenuRepository
    {
        Task<IEnumerable<Menu>> GetMenuAsync();
        Task<Menu> GetMenuItemByIdAsync(int id);
        Task<Menu> CreateMenuItemAsync(Menu menu);
        void DeleteMenuItem(Menu menu);
        Task<bool> SaveAllAsync();
    }
}
