namespace Saydalia_Online.Models
{
	public class Category
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public virtual List<Medicine> Medicines { get; set; }

	}
}
