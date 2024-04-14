namespace API.Entities
{
    public class Menu
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public uint Price { get; set; }

        public int Quantity { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public string CalculationUnit { get; set; }
    }
}
