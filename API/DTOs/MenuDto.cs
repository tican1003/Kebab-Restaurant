using API.Entities;

namespace API.DTOs
{
    public class MenuDto
    {
        public string Name { get; set; }
        public uint Price { get; set; }
        public int Quantity { get; set; }
        public string CalculationUnit { get; set; }
    }
}
