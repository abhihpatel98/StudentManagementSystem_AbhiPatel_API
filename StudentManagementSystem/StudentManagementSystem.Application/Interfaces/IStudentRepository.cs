using StudentManagementSystem.Domain.Entities;

namespace StudentManagementSystem.Application.Interfaces
{
    public interface IStudentRepository
    {
        Task<Student?> GetByIdAsync(int id);
        Task<Student?> GetByEmailOrPhoneAsync(string email, string phone);
        Task<List<Student>> GetAllAsync(int pageNumber, int pageSize, string? searchTerm = null);
        Task AddAsync(Student student);
        Task UpdateAsync(Student student);
        Task DeleteAsync(Student student);
        Task SaveChangesAsync();
    }
}
