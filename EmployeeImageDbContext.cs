using EmployeeImageUploadAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeImageUploadAPI
{
    public class EmployeeImageDbContext : DbContext
    {
        public EmployeeImageDbContext(DbContextOptions<EmployeeImageDbContext> options) : base (options)
        {

        }
        public DbSet<EmployeeImage> EmployeeImages { get; set; }
    }
}
