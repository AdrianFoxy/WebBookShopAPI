using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebBookShopAPI.Data.Interfaces;

namespace WebBookShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        public IGenreRepository _context;
        public GenreController(IGenreRepository context)
        {
            _context = context;
        }

        [HttpGet("all-genres")]
        public async Task<IActionResult> GetAllGenres()
        {
            var response = await _context.GetAllGenresAsync();
            return Ok(response);
        }
    }
}
