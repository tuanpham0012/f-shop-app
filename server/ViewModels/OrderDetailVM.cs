namespace ShopAppApi.ViewModels;

public partial class OrderDetailVM
{
    public long Id { get; set; }

    public long ProductId { get; set; }

    public long SkuId { get; set; }

    public string? ProductName { get; set; }

    public double UnitPrice { get; set; }

    public int Quantity { get; set; }

    public double TotalAmount { get; set; }

    public double DiscountAmount { get; set; }

    public long OrderId { get; set; }

    public double? UnitDiscount { get; set; }

    public double? TaxFee { get; set; }

    public virtual ProductVM Product { get; set; } = null!;

    public virtual SkuVM Sku { get; set; } = null!;
}
