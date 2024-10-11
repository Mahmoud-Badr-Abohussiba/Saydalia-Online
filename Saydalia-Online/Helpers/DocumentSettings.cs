namespace Saydalia_Online.Helpers
{
    public static class DocumentSettings
    {
        public async  static Task<string> UploadFile(IFormFile file, string folderName)
        {

            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", folderName);

            string fileName = $"{Guid.NewGuid()}"+"_"+$"{file.FileName}";

            string filePath = Path.Combine(folderPath, fileName);

            using var fs = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(fs);

            return fileName;
        }

        public static bool DeleteFile(string fileName, string folderName)
        {
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", folderName);
            string filePath = Path.Combine(folderPath, fileName);
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
                return true;
            }
            return false;
        }
    }
}
