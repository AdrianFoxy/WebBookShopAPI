using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebBookShopAPI.Data.Cart;
using WebBookShopAPI.Data.Dtos;
using WebBookShopAPI.Data.Models;
using WebBookShopAPI.Data.Repositories;
using WebBookShopAPI.Data.Specifications;

namespace WebBookShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly ShoppingCart _shoppingCart;
        private readonly IGenericRepository<Book> _bookRepository;
        private readonly IMapper _mapper;

        public ShoppingCartController(ShoppingCart shoppingCart, IGenericRepository<Book> booksRepo, IMapper mapper)
        {
            _shoppingCart = shoppingCart;
            _bookRepository = booksRepo;
            _mapper = mapper;
        }

        [HttpGet("get-shopping-cart")]
        public ActionResult<ShopCartDto> GetShoppingCart()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItem = items;

            var data = _mapper.Map<List<ShoppingCartItemDto>>(items);

            var responce = new ShopCartDto()
            {
                ShoppingCartItemDto = data,
                ShopCartTotal = _shoppingCart.GetShoppingCartTotal(),
                TotalItems = _shoppingCart.GetShoppingCartItemsSummary()
            };


            return Ok(responce);
        }

        [HttpPost("add-item-to-cart")]
        public async Task<IActionResult> AddItemToShoppingCart(int id)
        {

            var item = await _bookRepository.GetByIdAsync(id);

            if (item != null)
            {
                _shoppingCart.AddItemToCart(item);
            }
            return RedirectToAction(nameof(GetShoppingCart));
        }

        [HttpPost("remove-item-from-cart")]
        public async Task<IActionResult> RemoveItemFromShoppingCart(int id)
        {
            var item = await _bookRepository.GetByIdAsync(id);

            if (item != null)
            {
                _shoppingCart.RemoveItemFromCart(item);
            }
            return RedirectToAction(nameof(GetShoppingCart));
        }

        [HttpPost("remove-all-choosen-items-from-cart")]
        public async Task<IActionResult> RemoveAllChoosenItemsFromShoppingCart(int id)
        {
            var item = await _bookRepository.GetByIdAsync(id);

            if (item != null)
            {
                _shoppingCart.RemoveAllChoosenItemsFromCart(item);
            }
            return RedirectToAction(nameof(GetShoppingCart));
        }

        [HttpPost("clear-the-whole-cart")]
        public async Task<IActionResult> ClearTheCart()
        {
            await _shoppingCart.ClearShoppingCartAsync();
            return RedirectToAction(nameof(GetShoppingCart));
        }



    }
}
