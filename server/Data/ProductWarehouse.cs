using System;
using System.Collections.Generic;

namespace ShopAppApi.Data;

public partial class ProductWarehouse
{
    public long Id { get; set; }

    public long SkuId { get; set; }

    public long WarehouseId { get; set; }

    public int Stock { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Sku Sku { get; set; } = null!;

    public virtual Warehouse Warehouse { get; set; } = null!;
}
