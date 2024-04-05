using API.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Interfaces
{
    public interface IItemRepository
    {
        Task<IEnumerable<Item>> GetItemsAsync();
        Task<Item> GetItemByIdAsync(int id);
        Task<Item> CreateItemAsync(Item item);
        void DeleteItem(Item item);
        Task<bool> SaveAllAsync();
        Task GetItemByIdAsync(object id);
    }
