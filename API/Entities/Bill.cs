namespace API.Entities
{
    public class Bill
    {
        public int Id { get; set; }
        public Order Order { get; set; }
        public DateTime TimeOut { get; set; }
        public bool IsSucces {  get; set; }
        public Payment Payment { get; set; }
    }
}
