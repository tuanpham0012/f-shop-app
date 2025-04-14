namespace ShopAppApi.Request
{
    public class AddCartRequest
    {
        public long? CustomerId { get; set; }
        public long SkuId { get; set; }
        public int Quantity { get; set; }

    }
    public class UpdateCartRequest
    {
        public long? CustomerId { get; set; }
        public long? SkuId { get; set; }
        public int Quantity { get; set; }
        public int Type { get; set; } // 0:cập nhật theo số lượng 1: plus, 2: minus
    }

    public class DeleteCartRequest
    {
        public List<long> Ids { get; set; } = [];
    }

    public class CheckoutRequest
    {
        public List<long> CartIds { get; set; } = [];
        public long ShippingUnitId { get; set; }
        public long PaymentMethodId { get; set; }
        public long CustomerId { get; set; }
    }
}