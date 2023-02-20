 using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.PhotoStock.Dtos;
using Shared.ControllerBases;
using Shared.DTOs;

namespace Service.PhotoStock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : CustomBaseController
    {
        [HttpPost]
        public async Task<IActionResult> PhotoSave(IFormFile photo, CancellationToken cancellationToken)
        {
            if (photo is not null&&photo.Length>0)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photo.FileName);
                using var stream = new FileStream(path, FileMode.Create);
                await photo.CopyToAsync(stream, cancellationToken);
                var returnpath = photo.FileName;
                PhotoDto photoDto = new() { Url = returnpath };
                return CreateActionResult(Response<PhotoDto>.Success(photoDto, 200));
            }
            return CreateActionResult(Response<PhotoDto>.Fail("photo is empty", 400));
        }

        [HttpDelete]
        public IActionResult PhotoDelete(string photoUrl)
        {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photoUrl);
            if (!System.IO.File.Exists(path))
            {
                return CreateActionResult(Response<NoContent>.Fail("photo not found", 404)); 
            }
            System.IO.File.Delete(path);
            return CreateActionResult(Response<NoContent>.Success(204));
        }
    }
}
