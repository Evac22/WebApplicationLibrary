using Microsoft.AspNetCore.Mvc;
using WebApplicationLibrary.Models;

namespace WebApplicationLibrary.Controllers
{
    [ApiController]
    [Route("api/Genres")]
    public class GenreController : Controller
    {
        private readonly List<Genre> _genres;

        public GenreController(LibraryContextInitializer contextInitializer)
        {
            _genres = contextInitializer.Genres;
        }

        [HttpGet]
        public IActionResult GetGenres()
        {
            return Ok(_genres);
        }
    }
}