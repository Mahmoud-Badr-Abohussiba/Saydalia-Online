namespace Saydalia_Online.Helpers
{
    public static class DocumentSettings
    {
        public async static Task<string> UploadFile(IFormFile file, string folderName)
        {

            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", folderName);

            string fileName = $"{Guid.NewGuid()}{file.FileName}";

            string filePath = Path.Combine(folderPath, fileName);

            using var fs = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(fs);

            return fileName;
        }
    }
}
