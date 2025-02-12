using System.ComponentModel.DataAnnotations;

namespace ShopAppApi.Request
{
    public class StoreMenuRequest
    {
        [Required]
        public string Title { get; set; } = null!;

        public string? Url { get; set; }

        public string? Icon { get; set; }

        public int? ParentId { get; set; }

        public bool? GroupMenu { get; set; }

        public bool? Hidden { get; set; }
    }

    public class UpdateMenuRequest
    {
        [Required]
        public string Title { get; set; } = null!;

        public string? Url { get; set; }

        public string? Icon { get; set; }

        public int Lft { get; set; }

        public int Rgt { get; set; }

        public int? ParentId { get; set; }

        public bool? Hidden { get; set; }

        public bool? GroupMenu { get; set; }
    }
}