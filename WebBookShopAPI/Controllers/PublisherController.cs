using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebBookShopAPI.Data.Interfaces;
using WebBookShopAPI.Data.Models;

namespace WebBookShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        private readonly IGenericRepository<Publisher> _publisherRepository;

        public PublisherController(IGenericRepository<Publisher> publisherRepository)
        {
            _publisherRepository= publisherRepository;
        }

        [HttpGet("get-all-publishers")]
        public async Task<ActionResult<Publisher>> GetAllPublishers()
        {
            var response = await _publisherRepository.GetAllAsync();
            return Ok(response);
        }
    }
}
