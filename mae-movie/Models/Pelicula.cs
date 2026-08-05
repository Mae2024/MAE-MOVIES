using System.Reflection.Metadata.Ecma335;

namespace mae_movie.Models
{
    public class Pelicula
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public DateTime FechaLanzamiento { get; set; }
        public int MinutosDuracion { get; set; }
        public string Sinopsis { get; set; }
        public string PosterUrl { get; set; }
        public int GeneroId { get; set; }
        public int PromedioRating { get; set; }
        public Plataforma? Plataforma { get; set; }
        public int PlataformaId { get; set; }
        public Genero? Genero { get; set; }
        public List<Review>? ListaReview { get; set; }
        public List<Favorito> UsuarioFavorito { get; set; }



    }
}
