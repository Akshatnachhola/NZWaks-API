using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.DTO_s;
using NZWalksAPI.Repository;
using static System.Net.Mime.MediaTypeNames;

namespace NZWalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }
        //Post:/api/Images/Upload
        [HttpPost]
        [Route("Upload")]

        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto request)
        {
            ValidateFileUpload(request);

            if(ModelState.IsValid)
            {
                //Convert DTo to Domain Model 

                var imageDomainModel = new Models.Domain.Image
                {
                    File = request.File,
                    FileExtension = Path.GetExtension(request.File.FileName),
                    FileSizeInBytes = request.File.Length,
                    FileDescription = request.FileDescription,

                };
                //User repository to upload Image

                await imageRepository.Upload(imageDomainModel);

                return Ok(imageDomainModel);


            }

            return BadRequest(ModelState);

        }
          private void ValidateFileUpload (ImageUploadRequestDto request)
        {
            var allowedExtensions = new String[] { ".jpg,", ",jpeg", ".png" };

            if (!allowedExtensions.Contains(Path.GetExtension(request.File.FileName)) )
            {

                ModelState.AddModelError("file", "Unsupported file Extension");
            }

            if(request.File.Length> 1048560)
            {
                ModelState.AddModelError("file", "FIle Size More then 10 Mb please upload a smaller size file");
            }

        }

    }
}

