using Microsoft.EntityFrameworkCore;

namespace StudentManagementSystem.Infrastructure.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
    }
}
