using StudentManagementSystem.Application.DTOs;
using StudentManagementSystem.Application.Interfaces;
using StudentManagementSystem.Domain.Entities;

namespace StudentManagementSystem.Application.Services
{
    public class StudentService(
        IStudentRepository studentRepository,
        IClassRepository classRepository) : IStudentService
    {
        private readonly IStudentRepository _studentRepository = studentRepository;
        private readonly IClassRepository _classRepository = classRepository;

        public async Task<StudentDto> CreateStudentAsync(CreateStudentDto dto)
        {
            // Check duplicate email or phone
            var existingStudent =
                await _studentRepository.GetByEmailOrPhoneAsync(dto.EmailId, dto.PhoneNumber);

            if (existingStudent != null)
                throw new Exception("Student with same Email or Phone already exists.");

            var student = new Student
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                PhoneNumber = dto.PhoneNumber,
                EmailId = dto.EmailId
            };

            await AssignClassesAsync(student, dto.ClassIds);

            await _studentRepository.AddAsync(student);
            await _studentRepository.SaveChangesAsync();

            return MapToDto(student);
        }

        public async Task<StudentDto?> GetStudentByIdAsync(int id)
        {
            var student = await _studentRepository.GetByIdAsync(id);
            if (student == null) return null;

            return MapToDto(student);
        }

        public async Task<List<StudentDto>> GetAllStudentsAsync(
            int pageNumber,
            int pageSize,
            string? searchTerm = null)
        {
            var students =
                await _studentRepository.GetAllAsync(pageNumber, pageSize, searchTerm);

            return students.Select(MapToDto).ToList();
        }

        public async Task<StudentDto> UpdateStudentAsync(int id, UpdateStudentDto dto)
        {
            var student = await _studentRepository.GetByIdAsync(id);
            if (student == null)
                throw new Exception("Student not found.");

            var duplicate =
                await _studentRepository.GetByEmailOrPhoneAsync(dto.EmailId, dto.PhoneNumber);

            if (duplicate != null && duplicate.Id != id)
                throw new Exception("Another student with same Email or Phone exists.");

            student.FirstName = dto.FirstName;
            student.LastName = dto.LastName;
            student.PhoneNumber = dto.PhoneNumber;
            student.EmailId = dto.EmailId;

            // Clear old class mappings
            student.StudentClasses.Clear();

            await AssignClassesAsync(student, dto.ClassIds);

            await _studentRepository.UpdateAsync(student);
            await _studentRepository.SaveChangesAsync();

            return MapToDto(student);
        }

        public async Task<bool> DeleteStudentAsync(int id)
        {
            var student = await _studentRepository.GetByIdAsync(id);
            if (student == null) return false;

            await _studentRepository.DeleteAsync(student);
            await _studentRepository.SaveChangesAsync();
            return true;
        }

        // ----------------- Helpers -----------------

        private async Task AssignClassesAsync(Student student, List<int> classIds)
        {
            if (classIds == null || !classIds.Any()) return;

            foreach (var classId in classIds.Distinct())
            {
                var cls = await _classRepository.GetByIdAsync(classId);
                if (cls == null)
                    throw new Exception($"Class with Id {classId} does not exist.");

                student.StudentClasses.Add(new StudentClass
                {
                    ClassId = classId,
                    Student = student
                });
            }
        }

        private static StudentDto MapToDto(Student student)
        {
            return new StudentDto
            (
                student.Id,
                student.FirstName,
                student.LastName,
                student.EmailId,
                student.PhoneNumber,
                student.StudentClasses.Select(c => c.Class.Name).ToArray()
            );
        }
    }
}
