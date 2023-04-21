using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebBookShopAPI.Data.Dtos;
using WebBookShopAPI.Data.Errors;
using WebBookShopAPI.Data.Helpers;
using WebBookShopAPI.Data.Interfaces;
using WebBookShopAPI.Data.Models;
using WebBookShopAPI.Data.Specifications;

namespace WebBookShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {

        private readonly IGenericRepository<Book> _bookRepository;
        private readonly IGenericRepository<Genre> _genreRepository;
        private readonly IGenericRepository<Author> _authorRepository;
        private readonly IMapper _mapper;

        public BookController(IGenericRepository<Book> booksRepo, IGenericRepository<Genre> genreRepo, IGenericRepository<Author> authorRepo,
            IMapper mapper)
        {
            _bookRepository = booksRepo;
            _genreRepository = genreRepo;
            _authorRepository = authorRepo;
            _mapper = mapper;

        }


        [HttpGet("catalog_books")]
        public async Task<ActionResult<Pagination<BookInCatalogDto>>> GetAllBooksCatalog([FromQuery] BookSpecParams bookParams)
        {

            var spec = new BookWithAllInfoSpecification(bookParams);

            var countSpec = new BookWithFiltersCountSpecification(bookParams);
            var totalItems = await _bookRepository.CountAsync(countSpec);

            var response = await _bookRepository.ListAsync(spec);
            var data = _mapper
                .Map<IReadOnlyList<Book>, IReadOnlyList<BookInCatalogDto>>(response);


            //return Ok(response);
            // return _mapper.Map<Book, BookInCatalogDto>(response);
            return Ok(new Pagination<BookInCatalogDto>(bookParams.PageIndex, bookParams.PageSize, totalItems, data));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)] // ProducesResponseType показывает вариант ответов в свагере
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SingleBookDto>> GetBookById(int id)
        {
            var spec = new BookWithAllInfoSpecification(id);

            var response = await _bookRepository.GetEntityWithSpec(spec);

            if (response == null) return NotFound(new ApiResponse(404));
            return Ok(_mapper.Map<Book, SingleBookDto>(response));
        }

        [HttpGet("genres")]
        public async Task<IActionResult> GetAllGenres()
        {
            var response = await _genreRepository.GetAllAsync();
            return Ok(response);
        }

        [HttpGet("authors")]
        public async Task<IActionResult> GetAllAuthors()
        {
            var response = await _authorRepository.GetAllAsync();
            return Ok(response);
        }

    }
}
