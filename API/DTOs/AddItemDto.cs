namespace API.DTOs
{
    public class AddItemDto
    {
        public int OrderId { get; set; }
        public string Name { get; set; }
        public uint Price { get; set; }
        public int Quantity { get; set; }
        public string CaculationUnit { get; set; }
        public bool IsSuccess { get; set; } = false;
    }
}
