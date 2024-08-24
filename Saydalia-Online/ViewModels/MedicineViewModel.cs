using Saydalia_Online.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Saydalia_Online.ViewModels
{
    public class MedicineViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is Required!")]
        [MaxLength(50, ErrorMessage = "Max Lenngth is 50 Char")]
        [MinLength(2, ErrorMessage = "Min Length is 2")]
        [DataType("varchar")]
        public string Name { get; set; }
        public IFormFile Image { get; set; }
        public string? ImageName { get; set; }
        public string? Description { get; set; }
        [Required(ErrorMessage = "Price is Required!")]

        public int Price { get; set; }
        public int Stock { get; set; }
        [ForeignKey("Categories")]
        public int? Cat_Id { get; set; }
        public Category? Categories { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
    }
}
