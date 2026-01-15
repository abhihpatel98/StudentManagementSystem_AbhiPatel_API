using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace StudentManagementSystem.Application.DTOs
{
    public class UpdateStudentDto
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } = null!;

        [Required]
        [RegularExpression(@"^\d{10}$")]
        public string PhoneNumber { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string EmailId { get; set; } = null!;

        public List<int> ClassIds { get; set; } = new List<int>();
    }
}
