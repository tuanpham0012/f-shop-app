namespace ShopAppApi.ViewModels
{
    public partial class VariantVM
    {
        public long Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public long ProductId { get; set; }

        public long SkuId { get; set; }

        public long OptionId { get; set; }

        public long OptionValueId { get; set; }

        public virtual OptionVM? Option { get; set; } = null;

        public virtual OptionValueVM? OptionValue { get; set; } = null;
    }
}