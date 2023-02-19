using System.ComponentModel.DataAnnotations;

namespace Student_Management__InfoDevelopers_.Models
{
	public class students
	{
		[Key]
		public int Sno { get; set; }
		[Required]
		public int EnrolledYear { get; set; }
		[Required]
		public string Faculty { get; set; }
		[Required]
		public string Course { get; set; }
		[Required]
		public string FirstName { get; set; }
		public string MiddleName { get; set; }
		[Required]
		public string LastName { get; set; }
		[Required]
		public int Age { get; set; }
		[Required]
		public string Address { get; set; }
		[Required]
		[RegularExpression("^[0-9]*$", ErrorMessage ="Enter Valid Contact Number")]
		public string Contact { get; set; }

	}
}

