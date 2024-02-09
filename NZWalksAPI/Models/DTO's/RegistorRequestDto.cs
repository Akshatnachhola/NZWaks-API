using System.ComponentModel.DataAnnotations;

namespace NZWalksAPI.Models.DTO_s
{
    public class RegistorRequestDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public String Username { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]

        public String Password { get; set; }

        

        public String Roles { get; set; } 
    }
}
