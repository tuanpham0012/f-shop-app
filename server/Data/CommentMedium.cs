using System;
using System.Collections.Generic;

namespace ShopAppApi.Data;

public partial class CommentMedium
{
    public long Id { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
