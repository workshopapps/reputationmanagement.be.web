using src.Entities;

namespace src.Services;

public interface IAnonContactRepository
{
    public void SendMessage(ContactUs contactus);
}