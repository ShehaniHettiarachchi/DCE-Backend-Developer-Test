using System.ComponentModel.DataAnnotations;

namespace DCE_Application.Models
{
	public class Product
	{
		[Key]
		public string ProductId { get; set; }
		[Required]
		public string ProductName { get; set; }
		public double? UnitPrice { get; set; }
		public string SupplierId { get; set; }
		public string CreatedOn { get; set; }
		public Int16  IsActive { get; set; } = 0;
	}
}
