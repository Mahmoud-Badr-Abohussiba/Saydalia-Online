namespace Saydalia_Online.Models
{
	public class Category
	{
		public int Id { get; set; }
		public string Name { get; set; }
        public virtual ICollection<Medicine> ? Medicines { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime ? UpdatedAt { get; set; }
    }
}
