using StudentManagementSystem.Application.DTOs;

namespace StudentManagementSystem.Application.Interfaces
{
    public interface IStudentService
    {
        Task<StudentDto> CreateStudentAsync(CreateStudentDto dto);
        Task<StudentDto> UpdateStudentAsync(int id, UpdateStudentDto dto);
        Task<bool> DeleteStudentAsync(int id);
        Task<StudentDto?> GetStudentByIdAsync(int id);
        Task<List<StudentDto>> GetAllStudentsAsync(int pageNumber, int pageSize, string? searchTerm = null);
    }
}
