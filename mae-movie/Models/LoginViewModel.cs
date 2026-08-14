using System.ComponentModel.DataAnnotations;

namespace mae_movie.Models
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Clave { get; set; }
    }
}
