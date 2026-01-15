using CsvHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.Application.DTOs;
using StudentManagementSystem.Application.Interfaces;
using System.Globalization;

namespace StudentManagementSystem.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/classes")]
    public class ClassesController(IClassService classService) : ControllerBase
    {
        private readonly IClassService _classService = classService;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _classService.GetAllClassesAsync());
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("import")]
        public async Task<IActionResult> Import(IFormFile file)
        {
            try
            {
                if (file.Length > 5 * 1024 * 1024)
                    return BadRequest("File size exceeds 5MB.");

                using var reader = new StreamReader(file.OpenReadStream());
                using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

                var records = csv.GetRecords<CreateClassDto>().ToList();
                await _classService.BulkImportClassesAsync(records);

                return Ok("Classes imported successfully.");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
