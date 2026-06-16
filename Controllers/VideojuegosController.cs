using Microsoft.AspNetCore.Mvc;
// Organizamos y agrupamos nuestro código en un espacio de nombres (namespace) para mantenerlo ordenado
namespace MiPrimeraApi.Controllers
{
    // 1. AbstrAction: Definimos qué es un Videojuego para nuestro sistema
    public class Videojuego 
    {
        // Al poner = string.Empty; le aseguramos a .NET que nunca seran null por defecto
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Genero { get; set; } = string.Empty;
        // ESTADO -> Filled - process - pending
        public string Estado { get; set; }  = string.Empty;
        // URL IMG PORTADA
        public string Imagen { get; set; } = string.Empty;
        // Plataforma : PC, XBOX, PLAY, NINTENDO
        public string Consola { get; set; } = string.Empty;
        // Puntaje personal
        public int Ranking { get; set; }
        // Notas del juego
        public string Descripcion { get; set; } = string.Empty;
    }

    [ApiController]
    [Route("api/[controller]")] // La ruta será: api/videojuegos
    public class VideojuegosController : ControllerBase
    {
        // Simulamos una base de datos en memoria (una lista)
        private static List<Videojuego> _misJuegos = new List<Videojuego>
        {
            new Videojuego { Id = 1, Titulo = "Hollow Knight", Genero = "Metroidvania", Estado = "Filled", Imagen = "https://i.namu.wiki/i/7u-TfLK_jeVwBwb5SozyAdgNv_2KuPgsjTKtbFsvLLoztdjaIBrG67RmxtzJzZo1PgnCf4kGIdY4QMhVww7Q1A.webp", Consola = "PC", Ranking = 100, Descripcion = "I gave up the game, two years ago I played it again and I fell in love with it" },
            new Videojuego { Id = 2, Titulo = "Assassin's Creed Valhalla", Genero = "Action / RPG", Estado = "process", Imagen = "https://m.media-amazon.com/images/I/81PYKLkWWLL._AC_UF1000,1000_QL80_.jpg", Consola = "PC", Ranking = 70, Descripcion = "I'm currently playining this game, I like it a lot but It's extremely extensive." },
            new Videojuego { Id = 3, Titulo = "HOGWARTS LEGACY", Genero = "Fantasy / RPG", Estado = "Filled", Imagen = "https://image.api.playstation.com/vulcan/ap/rnd/202503/2716/f6b1e4512ee6061913f7d604da8f5f39566be56ca32a68ee.png", Consola = "PC", Ranking = 80, Descripcion = "It's extremely extensive however, It is based on my favorite book saga = 'Harry Potter'" },
            new Videojuego { Id = 2, Titulo = "Silksong", Genero = "Action / RPG", Estado = "Filled", Imagen = "https://m.media-amazon.com/images/M/MV5BMjA4NWE1YWMtNjQ4ZC00Y2Q3LWFjMGEtNGVhM2FmYzJjMzM1XkEyXkFqcGc@._V1_FMjpg_UX1000_.jpg", Consola = "PC", Ranking = 100, Descripcion = "Silksong is my favorite game, I completed it all with more than 100 hours played and enjoyed." },
            new Videojuego { Id = 2, Titulo = "DOOM", Genero = "FPS", Estado = "Filled", Imagen = "https://m.media-amazon.com/images/M/MV5BOWEwNThjODUtYTkyZS00MmNmLTk1Y2EtOGU1YTg0NmNmNjYyXkEyXkFqcGc@._V1_FMjpg_UX1000_.jpg", Consola = "PC", Ranking = 70, Descripcion = "It's pretty interesting because I almost never play video games of this style" }
        };

        // 1. OBTENER TODOS (GET: api/videojuegos)
        [HttpGet]
        public ActionResult<List<Videojuego>> ObtenerTodos()
        {
            return Ok(_misJuegos); // Retorna un HTTP 200 con la lista
        }

        // 2. CREAR UN NUEVO VIDEOJUEGO (POST: api/videojuegos)
        [HttpPost]
        public ActionResult CrearVideojuego(Videojuego nuevoJuego)
        {
            nuevoJuego.Id = _misJuegos.Count > 0 ? _misJuegos.Max(j => j.Id) + 1 : 1; // Asigna un nuevo ID incremental
            _misJuegos.Add(nuevoJuego);
            return StatusCode(201, nuevoJuego); // Retorna un HTTP 201 Creado
        }
        // 3. ELIMINAR UN VIDEOJUEGO (DELETE: api/videojuegos/{id})
        [HttpDelete("{id}")]
        public ActionResult EliminarVideojuego(int id)
        {
            var juego = _misJuegos.FirstOrDefault(j => j.Id == id);
            if (juego == null) return NotFound("El videojuego no existe."); // Retorna un HTTP 404 si no se encuentra el juego

            _misJuegos.Remove(juego);
            return NoContent(); // Retorna un HTTP 204 No Content
        }
    }
}