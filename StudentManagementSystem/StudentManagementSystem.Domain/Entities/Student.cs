using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.Domain.Entities
{
    public class Student
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; } = null!;
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } = null!;
        [Required]
        [MaxLength(10)]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be 10 digits.")]
        public string PhoneNumber { get; set; } = null!;
        [Required]
        [EmailAddress]
        public string EmailId { get; set; } = null!;
        public ICollection<StudentClass> StudentClasses { get; set; } = new List<StudentClass>();
    }
}
