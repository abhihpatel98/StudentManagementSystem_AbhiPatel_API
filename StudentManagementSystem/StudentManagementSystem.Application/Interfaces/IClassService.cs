using StudentManagementSystem.Application.DTOs;

namespace StudentManagementSystem.Application.Interfaces
{
    public interface IClassService
    {
        Task<ClassDto> CreateClassAsync(CreateClassDto dto);
        Task<List<ClassDto>> GetAllClassesAsync();
        Task BulkImportClassesAsync(List<CreateClassDto> classes);
    }
}
