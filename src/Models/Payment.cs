using System.ComponentModel.DataAnnotations;

namespace src.Models;

public class Payment
{
    public string Name { get; set; }

    [EmailAddress]
    public string Email { get; set; }

    public int Amount { get; set; }
}