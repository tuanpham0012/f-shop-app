using System;
using System.Collections.Generic;

namespace ShopAppApi.Data;

public partial class OrderDetail
{
    public long Id { get; set; }

    public long ProductId { get; set; }

    public long SkuId { get; set; }

    public string? ProductName { get; set; }

    public double UnitPrice { get; set; }

    public int Quantity { get; set; }

    public double TotalAmount { get; set; }

    public double DiscountAmount { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public long OrderId { get; set; }

    public double? UnitDiscount { get; set; }

    public double? TaxFee { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;

    public virtual Sku Sku { get; set; } = null!;
}
