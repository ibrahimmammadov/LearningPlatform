using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Catalog.Dtos;
using Service.Catalog.Services;
using Shared.ControllerBases;

namespace Service.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : CustomBaseController
    {
        private readonly ICourseService courseService;

        public CourseController(ICourseService _courseService)
        {
            courseService = _courseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await courseService.GetAllAsync();
            return CreateActionResult(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var response = await courseService.GetByIdAsync(id);
           return CreateActionResult(response);
        }

        [HttpGet]
        [Route("/api/[controller]/GetAllByUserId/{userid}")]
        public async Task<IActionResult> GetAllByUserId(string userid)
        {
            var response = await courseService.GetAllByUserIdAsync(userid);
            return CreateActionResult(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CourseCreateDto courseCreateDto)
        {
            var response = await courseService.CreateAsync(courseCreateDto);
            return CreateActionResult(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update(CourseUpdateDto courseUpdateDto)
        {
            var response = await courseService.UpdateAsync(courseUpdateDto);
            return CreateActionResult(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await courseService.DeleteAsync(id);
            return CreateActionResult(response);
        }

    }
}
