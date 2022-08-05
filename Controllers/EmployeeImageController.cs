using EmployeeImageUploadAPI.DTOs;
using EmployeeImageUploadAPI.Models;
using EmployeeImageUploadAPI.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeImageUploadAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeImageController : ControllerBase
    {
        private readonly EmployeeImageDbContext _dbContext;
        private readonly IWebHostEnvironment _hostEnvironment;

        public EmployeeImageController(EmployeeImageDbContext dbContext, IWebHostEnvironment hostEnvironment)
        {
            _dbContext = dbContext;
            _hostEnvironment = hostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployeeImages()
        {

            var images = await _dbContext.EmployeeImages.Select(x => new EmployeeImageView
            {
                Id = x.Id,
                ImageSource = String.Format("{0}://{1}{2}/Images/{3}", Request.Scheme, Request.Host, Request.PathBase, x.Image)
            }).ToListAsync();
            return Ok(images);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetEmployeeImage(int id)
        {
            var image = await _dbContext.EmployeeImages.FirstOrDefaultAsync(x => x.Id == id);
            return Ok(image);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromForm] EmployeeImageDTO employee)
        {
             
            EmployeeImage employeeMap = new();
            employeeMap.Image = await SaveImage(employee.ImageFile);
            await _dbContext.EmployeeImages.AddAsync(employeeMap);
            await _dbContext.SaveChangesAsync();
            return Ok("Added");
        }

        [NonAction]
        public async Task<string> SaveImage(IFormFile imageFile)
        {
            string imageName = new String(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray()).Replace(' ', '-');
            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(imageFile.FileName);
            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, "Images", imageName);

            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }
            return imageName;
        }



    }
}
