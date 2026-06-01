using Microsoft.AspNetCore.Mvc;
// Organizamos y agrupamos nuestro código en un espacio de nombres (namespace) para mantenerlo ordenado
namespace MiPrimeraApi.Controllers
{
    // 1. Abstracción: Definimos qué es un Videojuego para nuestro sistema
    public class Videojuego 
    {
        public int Id { get; set; }
        public string Título { get; set; } = string.Empty;
        public string Género { get; set; } = string.Empty;
    }

    [ApiController]
    [Route("api/[controller]")] // La ruta será: api/videojuegos
    public class VideojuegosController : ControllerBase
    {
        // Simulamos una base de datos en memoria (una lista)
        private static List<Videojuego> _misJuegos = new List<Videojuego>
        {
            new Videojuego { Id = 1, Título = "Hollow Knight", Género = "Metroidvania" },
            new Videojuego { Id = 2, Título = "Assassin's Creed Valhalla", Género = "Acción/RPG" }
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