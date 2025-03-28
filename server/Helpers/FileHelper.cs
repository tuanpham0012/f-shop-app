using System.Configuration;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using RestSharp;
using ShopAppApi.Helpers.Interfaces;
using Slugify;

namespace ShopAppApi.Helpers
{
    public class FileHelper(IConfiguration configuration, IHttpContextAccessor httpContextAccessor) : IFileHelper
    {
        private readonly string driver = configuration["FileStorage:Driver"] ?? "local";
        private readonly string defaultImage = configuration["FileStorage:DefaultImage"];
        private readonly string SaveFolder = "Files";

        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly Dictionary<byte[], string> FileSignatures = new Dictionary<byte[], string>
    {
        { new byte[] { 0xFF, 0xD8, 0xFF }, "jpg" },  // JPEG
        { new byte[] { 0x89, 0x50, 0x4E, 0x47 }, "png" },  // PNG
        { new byte[] { 0x47, 0x49, 0x46 }, "gif" },  // GIF
        { new byte[] { 0x42, 0x4D }, "bmp" },  // BMP
        { new byte[] { 0x25, 0x50, 0x44, 0x46 }, "pdf" },  // PDF
        { new byte[] { 0x49, 0x44, 0x33 }, "mp3" },  // MP3
        { new byte[] { 0x50, 0x4B, 0x03, 0x04 }, "zip" }  // ZIP (bao gồm cả DOCX, XLSX, PPTX) "FileStorage.SaveFolder"
    };

        public DirectoryInfo GetFileFolder(string subFolder = "")
        {
            return new DirectoryInfo(Directory.GetCurrentDirectory() + $"/{SaveFolder}/{subFolder}");
        }

        public string GetFileFormat(byte[] fileBytes)
        {
            foreach (var signature in FileSignatures)
            {
                if (fileBytes.Take(signature.Key.Length).SequenceEqual(signature.Key))
                {
                    return signature.Value;
                }
            }

            throw new ArgumentException("Định dạng file không hợp lệ!");
        }

        public async Task<string> SaveFile(string fileBase64, string subFolder = "")
        {

            if (String.IsNullOrEmpty(fileBase64) || !Regex.IsMatch(fileBase64, @"^data:image\/[a-zA-Z]+;base64,"))
            {
                return fileBase64;
            }

            string newImgData = Regex.Replace(fileBase64, @"^data:image\/[a-zA-Z]+;base64,", string.Empty);

            byte[] fileBytes = Convert.FromBase64String(newImgData);
            return await SaveFile(fileBytes);
        }

        public async Task<string> SaveFile(byte[] fileBytes, string subFolder = "")
        {
            DirectoryInfo info = GetFileFolder(subFolder);
            if (!info.Exists)
            {
                info.Create();
            }

            string extension = GetFileFormat(fileBytes);
            string fileName = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString() + "_" + StringHelper.RandomString(16) + "." + extension;
            string path = Path.Combine(info.FullName, fileName);
            using (MemoryStream memoryStream = new MemoryStream(fileBytes))
            {
                using Stream streamToWriteTo = File.Open(path, FileMode.Create);
                memoryStream.Position = 0;
                await memoryStream.CopyToAsync(streamToWriteTo);
            }
            return fileName; 
        }

        public void DeleteFile(string? fileName)
        {
            if (string.IsNullOrEmpty(fileName)) return;
            var path = Path.Combine(GetFileFolder().FullName, fileName);
            if (File.Exists(path)) File.Delete(path);
        }

        public async Task<byte[]?> Download(string fileName)
        {
            string filePath = Path.Combine(GetFileFolder().FullName, fileName);
            if (!System.IO.File.Exists(filePath))
            {
                return null;
            }
            return await System.IO.File.ReadAllBytesAsync(filePath);
        }

        public string GetLink(string? FileName)
        {
            var context = _httpContextAccessor.HttpContext ?? throw new ArgumentException("error context !");
            string host = $"{context.Request.Scheme}://{context.Request.Host}";
            if (string.IsNullOrEmpty(FileName))
            {
                return defaultImage;
            }
            switch (driver)
            {
                case "local":
                    return $"{host}/files/download/{FileName}";
                default:
                    return $"{host}/files/download/{FileName}";
            }
        }

        public async void SaveHtmlFile(string content, string fileName)
        {
            DirectoryInfo info = GetFileFolder("HtmlFiles");
            if (!info.Exists)
            {
                info.Create();
            }
            string path = Path.Combine(info.FullName, fileName);
            await File.WriteAllTextAsync(path, content);
        }

        public async Task<string> GetPostContentAsync(string slug)
        {
            string fileName = $"{slug}.html";
            string filePath = Path.Combine(GetFileFolder("HtmlFiles").FullName, fileName);

            if (File.Exists(filePath))
            {
                return await File.ReadAllTextAsync(filePath);
            }
            return ""; // Hoặc ném Exception
        }
    }
}