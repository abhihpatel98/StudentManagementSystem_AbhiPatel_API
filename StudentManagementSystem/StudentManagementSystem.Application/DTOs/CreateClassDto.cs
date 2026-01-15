using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.Application.DTOs
{
    public class CreateClassDto
    {
        [Required] 
        [MaxLength(50)]
        public string Name { get; set; } = null!;

        [MaxLength(100)]
        public string Description { get; set; } = null!;
    }
}
