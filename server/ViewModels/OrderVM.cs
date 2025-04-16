using ShopAppApi.Data;

namespace ShopAppApi.ViewModels;

public partial class OrderVM
{
    public long Id { get; set; }

    public string Code { get; set; } = null!;

    public DateOnly OrderDate { get; set; }

    public DateOnly? DeliveryDate { get; set; }

    public long CustomerId { get; set; }

    public long? UserId { get; set; }

    public long PaymentMethodId { get; set; }

    public long ShippingUnitId { get; set; }

    public double TotalAmount { get; set; }

    public double DiscountAmount { get; set; }

    public double ShippingFee { get; set; }

    public string? ShippingAddress { get; set; }

    public string? ShippingPhone { get; set; }

    public string? ReceiverName { get; set; }

    public byte Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual CustomerVM Customer { get; set; } = null!;

    public virtual ICollection<OrderDetailVM> OrderDetails { get; set; } = [];

    public virtual PaymentMethodVM PaymentMethod { get; set; } = null!;

    public virtual ShippingUnitVM ShippingUnit { get; set; } = null!;

    public virtual User? User { get; set; }

    public string TextStatus { get; set; } = string.Empty;

    public int ProductCount { get; set; }
}
