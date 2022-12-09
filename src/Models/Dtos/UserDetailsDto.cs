namespace src.Models.Dtos
{
    public class UserDetailsDto
    {
        public string Email { get; set; }
        public string BusinessEntityName { get; set; }
        public string PhoneNumber { get; set; }
        public string? FullName { get; set; }
        public string? BusinessWebsite { get; set; }
        public string? BusinessDescription { get; set; }
    }
}
