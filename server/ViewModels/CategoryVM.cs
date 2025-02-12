using System;
using System.Collections.Generic;

namespace ShopAppApi.Data;

public partial class CategoryVM
{
    public long Id { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public int Lft { get; set; }

    public int Rgt { get; set; }

    public int? ParentId { get; set; }

    public bool? NotUse { get; set; }
    public string? Image { get; set; }
    public bool? IsPopular { get; set; }
    public bool? HidenMenu { get; set; }

    public int ProductCount { get; set; }
}

public partial class CategoryTreeVM
{
    public long Id { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public int Lft { get; set; }

    public int Rgt { get; set; }

    public int? ParentId { get; set; }

    public bool? NotUse { get; set; }
    public string? Image { get; set; }
    public bool? IsPopular { get; set; }
    public bool? HidenMenu { get; set; }

    public int ProductCount { get; set; }
    public List<CategoryTreeVM> Children { get; set; } = new();
}
