using System.ComponentModel.DataAnnotations;

namespace mae_movie.Models
{
    public class Genero
    {
        public  int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Descripcion { get; set; }
        public List<Pelicula>? PeliculasGenero { get; set; }
    }
}
