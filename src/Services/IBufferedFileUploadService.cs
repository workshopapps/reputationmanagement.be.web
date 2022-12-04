namespace src.Services
{
    public interface IBufferedFileUploadService
    {
        Task<string> SaveFile(IFormFile file, string typeOfDocument);
    }
}
