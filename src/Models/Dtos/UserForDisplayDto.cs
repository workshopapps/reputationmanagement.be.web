namespace src.Models.Dtos
{
    public class UserForDisplayDto
    {
        public string Id { get; set; } = "";
        public string Username { get; set; } = "";
        public string BusinessEntityName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string BusinessWebsite { get; set; } = string.Empty;
        public string BusinessDescription { get; set; } = string.Empty;
    }
}
