namespace ShopAppApi.ViewModels
{
    public class ProductImageVM
    {
        public long Id { get; set; }

        public string Code { get; set; } = null!;

        public long ProductId { get; set; }

        public string Path { get; set; } = null!;

        public string FileName { get; set; } = null!;

        public string Extension { get; set; } = null!;

        public byte Type { get; set; }

        public string Driver { get; set; } = null!;

        public bool? IsDeleted { get; set; }
    }
}
