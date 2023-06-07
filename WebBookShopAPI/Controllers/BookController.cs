using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
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
        private readonly IBookRepository _bookRepo;
        private readonly ITokenService _tokenService;


        public BookController(IGenericRepository<Book> booksRepo, IGenericRepository<Genre> genreRepo, IGenericRepository<Author> authorRepo,
            IMapper mapper, IBookRepository bookRepo, ITokenService tokenService)
        {
            _bookRepository = booksRepo;
            _genreRepository = genreRepo;
            _authorRepository = authorRepo;
            _mapper = mapper;
            _bookRepo = bookRepo;
            _tokenService = tokenService;
        }

/*        [HttpGet("get-recommedations-by-orders")]
        public async Task<ActionResult<BookInCatalogDto>> GetRecommedantionsByOrders()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var books = await _bookRepo.GetRecommedantiosByOrders(userId);
            return Ok(_mapper.Map<IReadOnlyList<BookInCatalogDto>>(books));
        }*/

        [HttpGet("get-recommedations-by-orders-with-pag")]
        public async Task<ActionResult<BookInCatalogDto>> GetRecommedantionsByOrdersWithPag([FromQuery] PaginationParams pagParams, string userId, DateTime? MinUploadDate, DateTime? MaxUploadDate)
        {

            if (MinUploadDate == null) MinUploadDate = new DateTime(1900, 6, 7);
            if (MaxUploadDate == null) MaxUploadDate = DateTime.Today;

            var books = await _bookRepo.GetRecommedantiosByOrders(userId, MinUploadDate, MaxUploadDate);
            var totalItems = books.Count();

            var data = books
                .Skip((pagParams.PageIndex - 1) * pagParams.PageSize)
                .Take(pagParams.PageSize)
                .ToList();

            var bookList = _mapper.Map<IReadOnlyList<BookInCatalogDto>>(data);


            return Ok(new Pagination<BookInCatalogDto>(pagParams.PageIndex, pagParams.PageSize, totalItems, bookList));
            //return Ok(data);

        }

        [HttpGet("get-recommedations-by-age")]
        public async Task<ActionResult<BookInCatalogDto>> GetRecommedantionsByAge()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var books = await _bookRepo.GetRecommedantiosByAgeGroup(userId);
            return Ok(_mapper.Map<IReadOnlyList<BookInCatalogDto>>(books));
        }

        [HttpGet("get-best-sells-recommendation")]
        public async Task<ActionResult<BookInCatalogDto>> GetBestSellsRecommendations()
        {
            var books = await _bookRepo.GetRecommedantionsBestSells();
            return Ok(_mapper.Map<IReadOnlyList<BookInCatalogDto>>(books));
        }

        [HttpGet("get-random-recommedations")]
        public async Task<ActionResult<BookInCatalogDto>> GetRandomRecommendations()
        {
            var books = await _bookRepo.GetRecommedationsRandom();
            return Ok(_mapper.Map<IReadOnlyList<BookInCatalogDto>>(books));
        }

        [HttpGet("Test-getPurchasedBooks")]
        public async Task<ActionResult<BookInCatalogDto>> GetPurschasedBooks()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var books = await _bookRepo.GetPurchasedBooks(userId);
            return Ok(books);

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
