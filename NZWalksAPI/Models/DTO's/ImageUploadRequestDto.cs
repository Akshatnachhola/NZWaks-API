using System.ComponentModel.DataAnnotations;

namespace NZWalksAPI.Models.DTO_s
{
    public class ImageUploadRequestDto
    {
        [Required]
        public IFormFile File { get; set; }

        [Required]
        public String FileName {  get; set; }

        public String? FileDescription { get; set; }




    }

}
