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

        public async Task<string> SaveFile(IFormFile file, string typeOfDocument, string pathToImage)
        {
            string filePath = await UploadFile.SaveAndReturnFileName(file, typeOfDocument, pathToImage);
            return filePath;
        }
    }
}
