namespace API.DTOs
{
    public class ItemDto
    {
        public string Name { get; set; }
        public uint Price { get; set; }
        public int Quantity { get; set; }
        public string CaculationUnit { get; set; }
        public bool IsSuccess { get; set; }
        public decimal ItemTotal { get; set; }
    }
}
