using API.Entities;

namespace API.DTOs
{
    public class MenuDto
    {
        public string Name { get; set; }
        public uint Price { get; set; }
        public int Quantity { get; set; } = 1;
        public string ImageUrl { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty ;
        public string CalculationUnit { get; set; } = "Cái";
        public int MenuId { get; set; }
    }
}
