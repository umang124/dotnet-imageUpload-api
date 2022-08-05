using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeImageUploadAPI.DTOs
{
    public class EmployeeImageDTO
    {
        public IFormFile ImageFile { get; set; }
    }
}
