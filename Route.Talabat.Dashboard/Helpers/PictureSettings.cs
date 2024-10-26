namespace Route.Talabat.Dashboard.Helpers
{
    public class PictureSettings
    {
        public static string UploadFile(IFormFile file, string folderName)
        {
            //1.Get Folder Path
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", folderName);
            //2. Set FileName Uniqe
            var fileName = Guid.NewGuid() + file.FileName;
            //3. Get File Path
            var filePath = Path.Combine(folderPath, fileName);
            //4.Save File As Stream
            var fs = new FileStream(filePath, FileMode.Create);
            //5. Copy My File Into Stream
            file.CopyTo(fs);
            //6. Return File Name
            return Path.Combine("images\\products", fileName);
        }

        public static void DeleteFile(string folderName, string fileName)
        {
            //1. Get File Path
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", folderName);
            //2. Check If The File Exist
            if (File.Exists(filePath))
            {
                //3. Delete File
                File.Delete(filePath);
            }
        }
    }
}
