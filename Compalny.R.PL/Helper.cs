namespace Compalny.R.PL
{
    public static class Helper
    {
        public static string UploadFile(IFormFile file, string folderName)
        {
            string folderpath = Path.Combine(Directory.GetCurrentDirectory(),@"wwwroot\Files",folderName);

            string filename = $"{Guid.NewGuid()}{file.FileName}";

            string filepath = Path.Combine(folderpath, filename);

          using  var filestream = new FileStream(filepath, FileMode.Create);
            file.CopyTo(filestream);

            return filename;


        }

        public static void DeleteFile(string filename, string folderName)
        {
            string filepath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Files", folderName, filename);

            if(File.Exists(filepath))
            {
                File.Delete(filepath);
            }
        }
    }
}
