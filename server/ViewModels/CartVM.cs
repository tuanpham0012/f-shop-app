namespace ShopAppApi.ViewModels
{
    public partial class CartVM
    {
        public long Id { get; set; }

        public long CustomerId { get; set; }

        public long ProductId { get; set; }

        public long SkuId { get; set; }
        public long TotalPrice { get; set; }

        public int Quantity { get; set; }

        public virtual ProductVM Product { get; set; } = null!;

        public virtual SkuVM Sku { get; set; } = null!;
    }
}