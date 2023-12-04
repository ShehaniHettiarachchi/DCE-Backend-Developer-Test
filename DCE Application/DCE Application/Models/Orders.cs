using System.ComponentModel.DataAnnotations;

namespace DCE_Application.Models
{
	public class Orders
	{
		[Key]
		public string OrderId { get; set; }
		[Required]
		public string ProductId { get; set; }
		public Int16 OrderStatus { get; set; }
		public Int16 OrderType { get; set; }
		public string OrderBy { get; set; }
		public string OrderedOn { get; set; }
		public string ShippedOn { get; set; }
		public Int16 IsActive { get; set; } = 0;
	}
}
