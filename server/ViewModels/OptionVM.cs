namespace ShopAppApi.ViewModels
{
    public partial class OptionVM
    {
        public long Id { get; set; }

        public string? Code { get; set; }

        public long ProductId { get; set; }

        public string Name { get; set; } = null!;

        public byte Visual { get; set; }

        public byte Order { get; set; }

        public virtual ICollection<OptionValueVM> OptionValues { get; set; } = [];
    }
}