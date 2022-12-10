using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace src.Entities;

public class ContactUs
{
    [JsonIgnore]
    public int Id { get; set; }

    [Required]
    public string Company { get; set; }

    [EmailAddress(ErrorMessage = "Please We need a valid email address so that we may contact you")]
    [Required]
    public string Email { get; set; }

    [Required]
    public string message { get; set; }
}