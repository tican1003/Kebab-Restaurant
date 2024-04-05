using API.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Interfaces
{
    public interface IBillRepository
    {
        Task<IEnumerable<Bill>> GetBillsAsync();
        Task<Bill> GetBillByIdAsync(int id);
        Task<Bill> CreateBillAsync(Bill bill);
        void DeleteBill(Bill bill);
        Task<bool> SaveAllAsync();
    }
}

