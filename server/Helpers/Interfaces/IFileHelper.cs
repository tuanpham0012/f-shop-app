namespace ShopAppApi.Helpers.Interfaces
{
    public interface IFileHelper
    {
        string GetFileFormat(byte[] fileBytes);

        Task<string> SaveFile(string? fileBase64);

        Task<string?> SaveFile(byte[] fileBytes);

        void DeleteFile(string? fileName);

        Task<byte[]> Download(string fileName);

        string GetLink(string? FileName);
    }
}