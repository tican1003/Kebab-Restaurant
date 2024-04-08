using API.Entities;

namespace API.DTOs
{
    public class BillDto
    {
        public Order Order { get; set; }
        public DateTime TimeOut { get; set; }
        public bool IsSuccess { get; set; }
        public Payment Payment { get; set; }
        public Role Role { get; set; }
    }
}
