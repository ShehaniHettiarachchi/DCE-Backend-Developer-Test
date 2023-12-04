using System.ComponentModel.DataAnnotations;

namespace DCE_Application.Models
{
	public class Customer
	{
		[Key]
		public string UserId { get; set; }
		public string Username { get; set; }
		public string Email { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string CreatedOn { get; set; }
		public Int16  IsActive { get; set; } = 0;
	}
}
