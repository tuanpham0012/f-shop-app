using System;
using System.Collections.Generic;

namespace ShopAppApi.Data;

public partial class Category
{
    public long Id { get; set; }

    public string Code { get; set; } = null!;

    public string? Image { get; set; }

    public string Name { get; set; } = null!;

    public int Lft { get; set; }

    public int Rgt { get; set; }

    public int? ParentId { get; set; }

    public bool? NotUse { get; set; }

    public bool? IsPopular { get; set; }

    public bool? HidenMenu { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
