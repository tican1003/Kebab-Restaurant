using API.DTOs;
using API.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Interfaces
{
    public interface IItemRepository
    {
        Task<IEnumerable<Item>> GetItemsAsync();
        Task<Item> GetItemByIdAsync(int id);
        Task<Item> GetItemByMenuIdAsync(int id);
        Task<IEnumerable<Item>> GetItemsByOrderIdAsync(int id);
        Task<bool> PlusItemAsync(OrderMenuIdDto om);
        Task<bool> MinusItemAsync(OrderMenuIdDto om);
        Task<Item> CreateItemAsync(Item item);
        void DeleteItem(Item item);
        Task<bool> SaveAllAsync();
        Task<bool> IsExist(int id);
        Task<bool> IsExistOrder(int id);
    }
}
