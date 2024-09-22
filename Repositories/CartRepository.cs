using api.Data;

namespace api.Repositories
{
    public class CartRepository(ApplicationDbContext _context) : ICartRepository
    {
        public async Task<Cart?> CreateCartAsync(Cart cart)
        {
            var existingCart = await _context.Carts
                .FirstOrDefaultAsync(c => c.ProductId == cart.ProductId && c.UserId == cart.UserId);

            if (existingCart != null)
            {
                existingCart.Quantity += cart.Quantity;
                return await UpdateCartAsync(existingCart);
            }

            await _context.Carts.AddAsync(cart);
            await _context.SaveChangesAsync();

            var createdCart = await _context.Carts
                .Include(c => c.Product)
                .FirstOrDefaultAsync(c => c.Id == cart.Id);

            return createdCart;
        }


        public async Task<Cart?> DeleteCartAsync(int id)
        {
            var cart = await _context.Carts.Include(x => x.Product).FirstOrDefaultAsync(x => x.Id == id);
            if (cart == null)
            {
                return null;
            }
            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync();
            return cart;
        }

        public async Task<List<Cart>> GetAllAsync(string userId)
        {
            return await _context.Carts.Include(x => x.Product).ToListAsync();
        }

        public async Task<Cart?> UpdateCartAsync(Cart cart)
        {
            var cartExist = await _context.Carts.Include(x => x.Product).FirstOrDefaultAsync(x => x.Id == cart.Id);
            if (cartExist == null)
            {
                return null;
            }
            if (cart.Quantity < 1)
            {
                return await DeleteCartAsync(cart.Id);
            }
            cartExist.Quantity = cart.Quantity;
            await _context.SaveChangesAsync();
            return cartExist;
        }
    }
}
