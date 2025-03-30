namespace ShopAppApi.ViewModels
{
    public partial class SkuVM
    {
        public long Id { get; set; }

        public long ProductId { get; set; }

        public string Barcode { get; set; } = null!;
        public string? ImageCode { get; set; }

        public string? ImagePath { get; set; }

        public double Price { get; set; }

        public string Name { get; set; } = null!;

        public int Stock { get; set; }

        public bool? IsEdited { get; set; } = false;

        // public virtual ICollection<Discount> Discounts { get; set; } = new List<Discount>();

        // public virtual ICollection<ProductWarehouse> ProductWarehouses { get; set; } = new List<ProductWarehouse>();

        public virtual ICollection<VariantVM> Variants { get; set; } = [];
    }
}