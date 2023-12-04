using System.ComponentModel.DataAnnotations;

namespace DCE_Application.Models
{
	public class Supplier
	{
		[Key]
		public string SupplierId { get; set; }
		[Required]
		public string SupplierName { get; set; }
		public string CreatedOn { get; set; }
		public Int16 IsActive { get; set; } = 0;
	}
}
