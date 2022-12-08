using System.ComponentModel.DataAnnotations;

namespace src.Models;

public class Payment
{
    public string OrderNo { get; set; }

    [EmailAddress]
    public string Email { get; set; }

    public int Amount { get; set; }
}