using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeImageUploadAPI.Models
{
    public class EmployeeImage
    {
        [Key]
        public int Id { get; set; }
        public string Image { get; set; } = string.Empty;
    }
}
