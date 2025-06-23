using System.ComponentModel.DataAnnotations;

namespace ShopAppApi.Request
{
    public class OrderRequest : BaseRequest
    {
        public long? CustomerId { get; set; } = null;
        public long? UserId { get; set; } = null;
        public long? PaymentMethodId { get; set; } = null;
        public long? ShippingUnitId { get; set; } = null;
        public int? Status { get; set; } = null;
    }

    public class StoreOrderRequest
    {
        [Required]
        public long CustomerId { get; set; }
        [Required]
        public long PaymentMethodId { get; set; }
        [Required]
        public long ShippingUnitId { get; set; }
        [Required]
        public string Address { get; set; } = string.Empty;
        [Required]
        public string PhoneNumber { get; set; } = string.Empty;
        public string ReceiverName { get; set; } = string.Empty;
        public string Note { get; set; } = string.Empty;
        public List<OrderDetailRequest> OrderDetails { get; set; } = new List<OrderDetailRequest>();
    }
    public class OrderDetailRequest
    {
        public long? Id { get; set; }
        [Required]
        public long SkuId { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}