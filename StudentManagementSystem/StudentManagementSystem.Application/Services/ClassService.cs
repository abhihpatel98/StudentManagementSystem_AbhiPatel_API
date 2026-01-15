using StudentManagementSystem.Application.DTOs;
using StudentManagementSystem.Application.Interfaces;
using StudentManagementSystem.Domain.Entities;

namespace StudentManagementSystem.Application.Services
{
    public class ClassService : IClassService
    {
        private readonly IClassRepository _classRepository;

        public ClassService(IClassRepository classRepository)
        {
            _classRepository = classRepository;
        }

        public async Task<ClassDto> CreateClassAsync(CreateClassDto dto)
        {
            var cls = new Class
            {
                Name = dto.Name,
                Description = dto.Description
            };

            await _classRepository.AddRangeAsync(new List<Class> { cls });
            await _classRepository.SaveChangesAsync();

            return new ClassDto
            (
                cls.Id,
                cls.Name,
                cls.Description
            );
        }

        public async Task<List<ClassDto>> GetAllClassesAsync()
        {
            var classes = await _classRepository.GetAllAsync();

            return classes.Select(c => new ClassDto
            (
                c.Id,
                c.Name,
                c.Description
            )).ToList();
        }

        public async Task BulkImportClassesAsync(List<CreateClassDto> classes)
        {
            if (classes == null || !classes.Any())
                throw new Exception("CSV file is empty or invalid.");

            var classEntities = classes.Select(c => new Class
            {
                Name = c.Name,
                Description = c.Description
            }).ToList();

            await _classRepository.AddRangeAsync(classEntities);
            await _classRepository.SaveChangesAsync();
        }
    }
}