using API.Entities;

namespace API.DTOs
{
    public class ItemDto
    {
        public string Name { get; set; }
        public uint Price { get; set; }
        public int Quantity { get; set; } = 1;
        public string CaculationUnit { get; set; } = "cái";
        public bool IsSuccess { get; set; } = true;
        public Order Order { get; set; }
        public int MenuId { get; set; }
    }
}
