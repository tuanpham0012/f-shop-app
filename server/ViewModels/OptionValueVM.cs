namespace ShopAppApi.ViewModels;
public partial class OptionValueVM
{
    public long Id { get; set; }
    public string? Code { get; set; }
    public long ProductId { get; set; }
    public long OptionId { get; set; }
    public string? Value { get; set; }
    public string? Label { get; set; }
    public string? Image { get; set; }
    public bool? IsEdited { get; set; } = false;

}