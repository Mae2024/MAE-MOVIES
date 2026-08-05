namespace mae_movie.Models
{
    public class Usuario
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string ImagenPerfilUrl { get; set; }
        public List<Favorito> ListaFavorito { get; set; }



    }
}
