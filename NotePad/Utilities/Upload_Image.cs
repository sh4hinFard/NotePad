namespace NotePad.Utilities
{
    public class Upload_Image
    {
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _webhostenviroment;
        public Upload_Image(Microsoft.AspNetCore.Hosting.IHostingEnvironment webHostEnvironment)
        {
            _webhostenviroment = webHostEnvironment;
        }
        public string upload(IFormFile file)
        {
            string wwwPath = this._webhostenviroment.WebRootPath;
            string contentPath = this._webhostenviroment.ContentRootPath;

            string path = Path.Combine(this._webhostenviroment.WebRootPath + "\\images\\NotesAndTasks\\");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            List<string> uploadedFiles = new List<string>();
            string fileName = Path.GetFileName(file.FileName);
            using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
            {
                file.CopyTo(stream);
                uploadedFiles.Add(fileName);
            }
            return file.FileName;
        }
    }
}
