using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace mae_movie.Models
{
    public class Usuario : IdentityUser
    {
        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }
        [Required]
        [StringLength(100)]
        public string Apellido { get; set; }
        [DataType(DataType.Date)]
        public DateTime FechaNacimiento { get; set; }
        public string ImagenPerfilUrl { get; set; }
        public List<Favorito>? PaliculasFavoritas { get; set; }
        public List<Review>? ReviewsUsusario { get; set; }



    }

    public class UsuarioViewModel
    {
        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }
        [Required]
        [StringLength(50)]
        public string Apellido { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
      
        public string Clave { get; set; }
        [PasswordPropertyText]
        public string ConfirmarClave { get; set; }
    }


}
