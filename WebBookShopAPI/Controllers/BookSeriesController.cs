using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebBookShopAPI.Data.Interfaces;
using WebBookShopAPI.Data.Models;

namespace WebBookShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookSeriesController : ControllerBase
    {
        private readonly IGenericRepository<BookSeries> _bookSeriesRepository;

        public BookSeriesController(IGenericRepository<BookSeries> bookSeriesRepository)
        {
            _bookSeriesRepository= bookSeriesRepository;
        }

        [HttpGet("get-all-book-series")]
        public async Task<ActionResult<BookSeries>> GetAllPublishers()
        {
            var response = await _bookSeriesRepository.GetAllAsync();
            return Ok(response);
        }
    }
}
