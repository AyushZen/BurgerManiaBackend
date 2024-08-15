using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_BurgerManiaBackend.Models
{
    public class BurgerOrderData
    {
        public BurgerOrderData()
        {
            BurgerId = Guid.NewGuid();
        }
        [Key]
        public Guid BurgerId { get; set; }
        [Required]
        [MaxLength(50)]
        public string? BurgerName { get; set; }
        [Required]
        public int BurgerPrice { get; set; }
        [Required]
        public string? BurgerImage { get; set; }
        [Required]
        public string? BurgerType { get; set; }
        [MaxLength(100)]
        public string? BurgerDesc { get; set; }
        [ForeignKey("OrdersData")]
        public Guid OrderId { get; set; }
        public int BurgerCount { get; set; } = 0;
    }
}
