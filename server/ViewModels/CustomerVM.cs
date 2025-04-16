using System;
using System.Collections.Generic;

namespace ShopAppApi.ViewModels;

public partial class CustomerVM
{
    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public byte Status { get; set; }

    public byte Gender { get; set; }
}
