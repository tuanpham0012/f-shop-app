using System.ComponentModel.DataAnnotations;

namespace ShopAppApi.Request
{
    public class StoreMenuRequest
    {
        [Required]
        public string Name { get; set; } = null!;

        public string? Path { get; set; }

        public string? Icon { get; set; }

        public int? ParentId { get; set; }

        public bool? GroupMenu { get; set; }

        public bool? Active { get; set; }
    }

    public class UpdateMenuRequest
    {
        [Required]
        public string Name { get; set; } = null!;

        public string? Path { get; set; }

        public string? Icon { get; set; }

        public int Lft { get; set; }

        public int Rgt { get; set; }

        public int? ParentId { get; set; }

        public bool? Active { get; set; }

        public bool? GroupMenu { get; set; }
    }
}