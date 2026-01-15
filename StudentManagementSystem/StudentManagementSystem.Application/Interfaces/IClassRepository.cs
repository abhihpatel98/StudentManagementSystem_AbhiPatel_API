using StudentManagementSystem.Domain.Entities;

namespace StudentManagementSystem.Application.Interfaces
{
    public interface IClassRepository
    {
        Task<Class?> GetByIdAsync(int id);
        Task<List<Class>> GetAllAsync();
        Task AddRangeAsync(List<Class> classes);
        Task SaveChangesAsync();
    }
}
