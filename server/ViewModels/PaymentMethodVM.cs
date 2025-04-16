﻿using System;
using System.Collections.Generic;

namespace ShopAppApi.ViewModels;

public partial class PaymentMethodVM
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Token { get; set; }

    public string Url { get; set; } = null!;

    public string? Description { get; set; }

    public string? Icon { get; set; }
}
