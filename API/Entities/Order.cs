using System.ComponentModel;

namespace API.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public AppUser User { get; set; }
        public bool IsActive { get; set; }
        public bool IsTakeAway { get; set; }
        public int TableNumber { get; set; }
        public DateTime TimeIn { get; set; }
    }
}
