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

    //public class LoginViewModel
    //{
    //    [Required(ErrorMessage = "El email es requerido")]
    //    [EmailAddress(ErrorMessage = "El email no es válido")]
    //    public string Email { get; set; }
    //    [Required(ErrorMessage = "La contraseña es requerida")]
    //    [DataType(DataType.Password)]
    //    public string Clave { get; set; }
    //    public bool Recordame { get; set; }
    //}

    public class RegistroViewModel
    {
        [Required(ErrorMessage = "Debes ingresar el nombre")]
        [StringLength(50)]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Debes ingresar el apellido")]
        [StringLength(50)]
        public string Apellido { get; set; }
        [EmailAddress(ErrorMessage = "El email no es válido")]
        [Required(ErrorMessage = "El email es requerido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "La contraseña es requerida")]
        [DataType(DataType.Password)]
        public string Clave { get; set; }
        [DataType(DataType.Password)]
        [Compare("Clave", ErrorMessage = "Las contraseñas no coinciden")]
        public string ConfirmarClave { get; set; }
    }

    public class MiPerfilViewModel
    {
        [Required(ErrorMessage = "Debes ingresar el nombre")]
        [StringLength(50)]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Debes ingresar el apellido")]
        [StringLength(50)]
        public string Apellido { get; set; }
        [EmailAddress(ErrorMessage = "El email no es válido")]
        [Required(ErrorMessage = "El email es requerido")]
        public string? Email { get; set; }
        public string? ImagenPerfil { get; set; }
    }

}
