using System;
using System.Collections.Generic;

namespace ShopAppApi.Data;

public partial class ProductImage
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

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Product Product { get; set; } = null!;
}
