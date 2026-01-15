using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.Domain.Entities
{
    public class Class
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;
        [MaxLength(100)]
        public string Description { get; set; } = null!;
        public ICollection<StudentClass> StudentClasses { get; set; } = new List<StudentClass>();
    }
}
