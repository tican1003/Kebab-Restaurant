namespace API.DTOs
{
    public class MemberDto
    {
        public int Id { get; set; }
        public string KnownAs { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string PhotoUrl { get; set; }
    }
}
