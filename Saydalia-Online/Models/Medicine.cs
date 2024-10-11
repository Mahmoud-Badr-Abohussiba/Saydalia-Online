using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Saydalia_Online.Models
{
	public class Medicine
	{
        [Display(Name = "No.")]
        public int Id { get; set; }

        [Display(Name = "name of medicine")]
        public string Name { get; set; }

        [Display(Name = "picture of Midicine")]
        public string ? ImageName { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Price/piece")]
        public int Price { get; set; }

        [Display(Name = "Quantity of stock")]
        public int	Stock { get; set; }
	
		[ForeignKey("Categories")]
		public int? Cat_Id { get; set; }
		
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime ? UpdatedAt { get; set; }
    

        [Display(Name = "Category Of Medicine")]

        public virtual Category? Categories { get; set; }

        public ICollection<OrderItem>? OrderItems { get; set; }
    }
}
