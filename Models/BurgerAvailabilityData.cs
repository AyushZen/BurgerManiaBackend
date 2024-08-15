using System.ComponentModel.DataAnnotations;

namespace API_BurgerManiaBackend.Models
{
    public class BurgerAvailabilityData
    {
        [Key]
        [Required]
        public string? BurgerId { get; set; }
        [Required]
        [MaxLength(50)]
        public string? BurgerName { get; set; }
        [Required]
        public List<int> BurgerPrice { get; set; } = new List<int>(3);
        [Required]
        public string? BurgerImage { get; set; }
        [Required]
        public string? BurgerType { get; set; }
        [MaxLength(100)]
        public string? BurgerDesc { get; set; }
        [Required]
        public List<string?> BurgerAvailableTypes { get; set; } = new List<string?>(3);
        public int BurgerCount { get; set; } = 0;

    }
}
