using System;
using System.Collections.Generic;

namespace ShopAppApi.Data;

public partial class ProductComment
{
    public long Id { get; set; }

    public long ProductId { get; set; }

    public long SkuId { get; set; }

    public long CustomerId { get; set; }

    public int Rating { get; set; }

    public string ProductInfo { get; set; } = null!;

    public string? Content { get; set; }

    public int? Likes { get; set; }

    public string? SellerResponse { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;

    public virtual Sku Sku { get; set; } = null!;
}
