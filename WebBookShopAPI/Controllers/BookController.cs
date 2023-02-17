using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebBookShopAPI.Data.Dtos;
using WebBookShopAPI.Data.Errors;
using WebBookShopAPI.Data.Models;
using WebBookShopAPI.Data.Repositories;
using WebBookShopAPI.Data.Specifications;

namespace WebBookShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        //public IBookRepository _context;
        //public BookController(IBookRepository context)
        //{
        //    _context = context;
        //}

        private readonly IGenericRepository<Book> _bookRepository;
        private readonly IGenericRepository<Genre> _genreRepository;
        private readonly IGenericRepository<Author> _authorRepository;
        private readonly IGenericRepository<CategoryGenre> _categorygenreRepository;
        private readonly IMapper _mapper;

        public BookController(IGenericRepository<Book> booksRepo, IGenericRepository<Genre> genreRepo, IGenericRepository<Author> authorRepo,
            IGenericRepository<CategoryGenre> categorygenreRepository, IMapper mapper) 
        {
            _bookRepository = booksRepo;
            _genreRepository = genreRepo;
            _authorRepository= authorRepo;
            _categorygenreRepository = categorygenreRepository;
            _mapper = mapper;

        }


        [HttpGet("catalog_books")]
        public async Task<IActionResult> GetAllBooksCatalog()
        {
            var spec = new BookWithAllInfoSpecification();

            var response = await _bookRepository.ListAsync(spec);
            //return Ok(response);
            // return _mapper.Map<Book, BookInCatalogDto>(response);
            return Ok(_mapper.Map<IReadOnlyList<Book>, IReadOnlyList<BookInCatalogDto>>(response));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)] // ProducesResponseType показывает вариант ответов в свагере
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BookInCatalogDto>> GetBookById(int id)
        {
            var spec = new BookWithAllInfoSpecification(id);

            var response = await _bookRepository.GetEntityWithSpec(spec);

            if (response == null) return NotFound(new ApiResponse(404));
            return Ok(_mapper.Map<Book, BookInCatalogDto>(response));
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

        [HttpGet("categoryGenres")]
        public async Task<IActionResult> GetAllCategoryGenres()
        {
            var response = await _categorygenreRepository.GetAllAsync();
            return Ok(response);
        }

    }
}
