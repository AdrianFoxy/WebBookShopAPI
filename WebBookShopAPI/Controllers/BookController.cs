using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebBookShopAPI.Data.Models;
using WebBookShopAPI.Data.Repositories;

namespace WebBookShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        public IBookRepository _context;
        public BookController(IBookRepository context)
        {
            _context = context;
        }

        [HttpGet("catalog_books")]
        public async Task<IActionResult> GetAllBooksCatalog()
        {
            var response = await _context.GetAllBooksCatalogAsync();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            var response = await _context.GetBookByIdAsync(id);
            return Ok(response);
        }

    }
}
