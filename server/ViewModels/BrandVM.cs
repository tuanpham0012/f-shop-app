namespace ShopAppApi.ViewModels
{
    public partial class BrandVM
    {
        public long Id { get; set; }

        public string Code { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string? Image { get; set; }
        public string? ImagePreview { get; set; }

        public bool? NotUse { get; set; }

        public int ProductCount { get; set; }
    }
}