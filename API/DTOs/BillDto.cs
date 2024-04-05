using API.Entities;

namespace API.DTOs
{
    public class BillDto
    {
        public Order Order { get; set; }
        public DateTime TimeOut { get; set; }
        public bool IsSucces { get; set; }
        public Payment Payment { get; set; }
        public int OrderId { get; internal set; }
        public int PaymentId { get; internal set; }
        public bool IsSuccess { get; internal set; }
    }
}
