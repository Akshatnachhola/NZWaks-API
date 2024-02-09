using System.ComponentModel.DataAnnotations;

namespace NZWalksAPI.Models.DTO_s
{
    public class UpdateRegionRequestDto
    {

        [Required]
        [MinLength(3, ErrorMessage = "Code has to be a minimun of three characters")]
        [MaxLength(3, ErrorMessage = "Code has to be a minimun of three characters")]
        public string Code { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public string? RegionImageUrl { get; set; }
    }
}
