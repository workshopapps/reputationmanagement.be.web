using src.Data;
using src.Entities;

namespace src.Services;

public class AnonContactUsRepo : IAnonContactRepository
{
    public readonly ApplicationDbContext _context;

    public AnonContactUsRepo(ApplicationDbContext context)
    {
        _context = context;
    }

    public void SendMessage(ContactUs contactus)
    {
        _context.ContactUs.Add(contactus);
        _context.SaveChanges();
    }
}