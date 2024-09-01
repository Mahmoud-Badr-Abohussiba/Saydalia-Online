using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Saydalia_Online.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public DateTime OrderDate { get; set; }     
        public decimal TotalAmount { get; set; }        
        public string Status { get; set; }

        // Navigation Properties
        [ForeignKey("User")]
        public string UserID { get; set; }
        public IdentityUser User { get; set; }                  
        public ICollection<OrderItem> OrderItems { get; set; } 
    }

}
