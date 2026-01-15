using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.Application.DTOs;
using StudentManagementSystem.Application.Interfaces;

namespace StudentManagementSystem.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/students")]
    public class StudentsController(IStudentService studentService) : ControllerBase
    {
        private readonly IStudentService _studentService = studentService;

        [HttpGet]
        public async Task<IActionResult> GetAll(
            int pageNumber = 1,
            int pageSize = 10,
            string? search = null)
        {
            try
            {
                var result = await _studentService
                .GetAllStudentsAsync(pageNumber, pageSize, search);
                return Ok(result);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var student = await _studentService.GetStudentByIdAsync(id);
                return student == null ? NotFound() : Ok(student);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateStudentDto dto)
        {
            try
            {
                var student = await _studentService.CreateStudentAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = student.Id }, student);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateStudentDto dto)
        {
            try
            {
                var student = await _studentService.UpdateStudentAsync(id, dto);
                return Ok(student);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deleted = await _studentService.DeleteStudentAsync(id);
                return deleted ? NoContent() : NotFound();
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
