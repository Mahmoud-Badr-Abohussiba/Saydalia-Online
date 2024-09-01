using System.ComponentModel.DataAnnotations.Schema;

namespace Saydalia_Online.Models
{
	public class Medicine
	{
		public int Id { get; set; }
		public string Name { get; set; }
        public string ? ImageName { get; set; }
		public string ? Description { get; set; }
		public int  Price { get; set; }
		public int Stock { get; set; }
		[ForeignKey("Categories")]
		public int? Cat_Id { get; set; }
		public Category ? Categories { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime ? UpdatedAt { get; set; }
    }
}
