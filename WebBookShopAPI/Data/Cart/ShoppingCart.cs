using Microsoft.EntityFrameworkCore;
using WebBookShopAPI.Data.Models;

namespace WebBookShopAPI.Data.Cart
{
    public class ShoppingCart
    {
        public AppDbContext _context { get; set; }
        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItem { get; set; }

        public ShoppingCart(AppDbContext context)
        {
            _context = context;
        }


        public static ShoppingCart GetShoppingCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session; // if not null
            var context = services.GetService<AppDbContext>();

            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId", cartId);


            return new ShoppingCart(context) { ShoppingCartId = cartId };
        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItem ?? (ShoppingCartItem = _context.ShoppingCartItem.Where(n => n.ShoppingCartId ==
            ShoppingCartId).Include(n => n.Book).ThenInclude(a => a.Author).ToList());
        }

        public int GetShoppingCartItemsSummary()
        {
            var total = _context.ShoppingCartItem.Where(n => n.ShoppingCartId == ShoppingCartId).Select(n => n.Amount).Sum();

            return total;
        }

        public double GetShoppingCartTotal()
        {

            var total = _context.ShoppingCartItem.Where(n => n.ShoppingCartId == ShoppingCartId).Select(n => n.Book.Price * n.Amount).Sum();

            return total;
        }
        public void AddItemToCart(Book book)
        {
            var shoppingCartItem = _context.ShoppingCartItem.FirstOrDefault(n => n.Book.Id == book.Id &&
            n.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem()
                {
                    ShoppingCartId = ShoppingCartId,
                    Book = book,
                    Amount = 1
                };
                _context.ShoppingCartItem.Add(shoppingCartItem);
            }
            else if (book.Amount <= shoppingCartItem.Amount)
            {
                shoppingCartItem.Amount = shoppingCartItem.Amount;
            }
            else
            {
                shoppingCartItem.Amount++;
            }
            _context.SaveChanges();
        }

        public void RemoveItemFromCart(Book book)
        {
            var shoppingCartItem = _context.ShoppingCartItem.FirstOrDefault(n => n.Book.Id == book.Id &&
            n.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                }
                else
                {
                    _context.ShoppingCartItem.Remove(shoppingCartItem);
                }
            }
            _context.SaveChanges();
        }

        public void RemoveAllChoosenItemsFromCart(Book book)
        {
            var shoppingCartItem = _context.ShoppingCartItem.FirstOrDefault(n => n.Book.Id == book.Id &&
            n.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem != null)
            {
                _context.ShoppingCartItem.Remove(shoppingCartItem);
            }
            _context.SaveChanges();
        }


        public async Task ClearShoppingCartAsync()
        {
            var items = await _context.ShoppingCartItem.Where(n => n.ShoppingCartId ==
            ShoppingCartId).ToListAsync();
            _context.ShoppingCartItem.RemoveRange(items);
            await _context.SaveChangesAsync();
        }

    }
}
