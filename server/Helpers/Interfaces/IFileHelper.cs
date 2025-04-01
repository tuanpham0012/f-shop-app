namespace ShopAppApi.Helpers.Interfaces
{
    public interface IFileHelper
    {
        string GetFileFormat(byte[] fileBytes);

        string SaveFile(string fileBase64, string subFolder = "");
        void SaveHtmlFile(string content, string fileName);

        string SaveFile(byte[] fileBytes, string subFolder   = "");

        void DeleteFile(string? fileName);

        Task<byte[]?> Download(string fileName);

        string GetLink(string? FileName);
        Task<string> GetPostContentAsync(string slug);
    }
}