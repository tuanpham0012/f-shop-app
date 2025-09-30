using System;
using System.Collections.Generic;

namespace ShopAppApi.Data;

public partial class Menu
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Path { get; set; }

    public string? Icon { get; set; }

    public byte Position { get; set; }

    public int Lft { get; set; }

    public int Rgt { get; set; }

    public int? ParentId { get; set; }

    public bool? Active { get; set; }

    public byte Type { get; set; }

    public bool? GroupMenu { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
