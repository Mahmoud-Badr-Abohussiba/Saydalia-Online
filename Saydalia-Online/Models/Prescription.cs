using System.ComponentModel.DataAnnotations;

namespace Saydalia_Online.Models
{
	public class Prescription
	{
		public int Id { get; set; }

		public string? Image { get; set; }

		public string Address { get; set; }

		public PrescriptionStatus Status { get; set; } = PrescriptionStatus.Pending;

		[MaxLength(1000)]
		public string? Comment { get; set; }

		public DateTime CreatedAt { get; set; } = DateTime.Now;

		public DateTime? UpdatedAt { get; set; }
	}

	public enum PrescriptionStatus
	{
		Pending,
		Processing,
		Delivered,
		NotAvailable
	}


}
