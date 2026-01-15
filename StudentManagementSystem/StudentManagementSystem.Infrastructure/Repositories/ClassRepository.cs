using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Application.Interfaces;
using StudentManagementSystem.Domain.Entities;
using StudentManagementSystem.Infrastructure.Data;

namespace StudentManagementSystem.Infrastructure.Repositories
{
    public class ClassRepository(ApplicationDbContext context) : IClassRepository
    {
        private readonly ApplicationDbContext _context = context;

        public async Task AddRangeAsync(List<Class> classes) => await _context.Classes.AddRangeAsync(classes);

        public async Task<List<Class>> GetAllAsync() => await _context.Classes.ToListAsync();

        public async Task<Class?> GetByIdAsync(int id) => await _context.Classes.FindAsync(id);

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}
