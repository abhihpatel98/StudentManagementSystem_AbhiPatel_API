using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Application.Interfaces;
using StudentManagementSystem.Domain.Entities;
using StudentManagementSystem.Infrastructure.Data;

namespace StudentManagementSystem.Infrastructure.Repositories
{
    public class StudentRepository(ApplicationDbContext context) : IStudentRepository
    {
        private readonly ApplicationDbContext _context = context;

        public async Task AddAsync(Student student) => await _context.Students.AddAsync(student);

        public async Task DeleteAsync(Student student) => _context.Students.Remove(student);

        public async Task<Student?> GetByIdAsync(int id) =>
            await _context.Students.Include(s => s.StudentClasses)
                                   .ThenInclude(sc => sc.Class)
                                   .FirstOrDefaultAsync(s => s.Id == id);

        public async Task<Student?> GetByEmailOrPhoneAsync(string email, string phone) =>
            await _context.Students.FirstOrDefaultAsync(s => s.EmailId == email || s.PhoneNumber == phone);

        public async Task<List<Student>> GetAllAsync(int pageNumber, int pageSize, string? searchTerm = null)
        {
            var query = _context.Students.Include(s => s.StudentClasses).ThenInclude(sc => sc.Class).AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(s =>
                    s.FirstName.Contains(searchTerm) ||
                    s.LastName.Contains(searchTerm) ||
                    s.EmailId.Contains(searchTerm));
            }

            return await query.Skip((pageNumber - 1) * pageSize)
                              .Take(pageSize)
                              .ToListAsync();
        }

        public Task UpdateAsync(Student student)
        {
            _context.Students.Update(student);
            return Task.CompletedTask;
        }

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}
