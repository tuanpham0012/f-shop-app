namespace ShopAppApi.Helpers.Interfaces
{
    public interface IFileHelper
    {
        public string GetFileFormat(byte[] fileBytes);

        public Task<string> SaveFile(string? fileBase64);

        public Task<string?> SaveFile(byte[] fileBytes);

        public void DeleteFile(string? fileName);

        public byte[]? Download(string fileName);

        public string getLink(string FileName);
    }
}