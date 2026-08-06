using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;

namespace mae_movie.Models
{
    public class Pelicula
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Titulo { get; set; }
        public DateTime FechaLanzamiento { get; set; }
        [Required]
        [Range(1, 500)]
        public int MinutosDuracion { get; set; }
        [StringLength(500)] 
        public string Sinopsis { get; set; }
        [Url]
        [Required]
        public string PosterUrl { get; set; }
        public int GeneroId { get; set; }
        [NotMapped] //para no impactar en la bd
        public int PromedioRating { get; set; }
        public Plataforma? Plataforma { get; set; }
        public int PlataformaId { get; set; }
        public Genero? Genero { get; set; }
        public List<Review>? ListaReview { get; set; }
        public List<Favorito>? UsuarioFavorito { get; set; }



    }
}
