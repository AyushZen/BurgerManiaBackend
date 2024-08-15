using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_BurgerManiaBackend.Models
{
    public class UserData
    {
        public UserData()
        {
            UserId = Guid.NewGuid();
        }
        [Key]
        public Guid UserId { get; set; }

        
        [Required]
        [MaxLength(10)]
        [MinLength(10)]
        public string Number { get; set; }
        [Required]
        public string Role {  get; set; }
    }
}
