using src.Helpers;

namespace src.Services
{
    public class BufferedFileUploadLocalService : IBufferedFileUploadService
    {
        public async Task<string> SaveFile(IFormFile file, string typeOfDocument)
        {
            string filePath = await UploadFile.SaveAndReturnFileName(file, typeOfDocument);
            return filePath;
        }
    }
}
