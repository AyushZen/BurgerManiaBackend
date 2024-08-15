

using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_BurgerManiaBackend.Models
{
    public class OrdersData
    {
        public OrdersData()
        {
            OrderId = Guid.NewGuid();
        }
        [Key]
        public Guid OrderId { get; set; }

        public DateTime OrderDateTime { get; set; }
 
        [Required]
        //[MaxLength(10)]
        //[MinLength(10)]
        [ForeignKey("UserData")]
        public Guid UserId { get; set; }
        public int TotalBillPrice { get; set; }
    }
}
