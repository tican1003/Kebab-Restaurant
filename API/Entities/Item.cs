namespace API.Entities
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public uint Price { get; set; }

        public int Quantity { get; set; }
        public string CaculationUnit {  get; set; }
        public Order Order { get; set; }
        public bool IsSuccess { get; set; }

        public decimal ItemTotal()
        {
            return this.Price * Quantity;
        }
    }
}
