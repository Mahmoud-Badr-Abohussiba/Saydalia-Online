using System.ComponentModel.DataAnnotations.Schema;

namespace Saydalia_Online.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; } 
        public decimal Price { get; set; }
        [ForeignKey("Order")]
        public int OrderID { get; set; }
        // Navigation Properties
        public Order Order { get; set; }
        [ForeignKey("Medicine")]
        public int MedicineID { get; set; }
        public Medicine Medicine { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime ? UpdatedAt { get; set; }
    }

}
