using Microsoft.AspNetCore.Identity;
using Saydalia_Online.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace Saydalia_Online.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime? OrderDate { get; set; }     
        public decimal? TotalAmount { get; set; }        
        public string Status { get; set; }
        public string? Address { get; set; }

        [EgyptianPhoneNumber(ErrorMessage = "Please enter a valid Egyptian phone number.")]
        public string? Phone { get; set; }

        // Navigation Properties
        [ForeignKey("User")]
        public string UserID { get; set; }
        public Saydalia_Online_AuthUser User { get; set; }                  
        public ICollection<OrderItem> OrderItems { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
    }

}
