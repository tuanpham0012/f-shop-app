namespace ShopAppApi.ViewModels
{
    public class ProductImageVM
    {
        public long Id { get; set; }

        public long ProductId { get; set; }

        public string Path { get; set; } = null!;

        public byte Type { get; set; }

        public string Driver { get; set; } = null!;

        public bool? Deleted { get; set; }
    }
}
