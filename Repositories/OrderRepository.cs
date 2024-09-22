using api.Data;

namespace api.Repositories
{
    public class OrderRepository(ApplicationDbContext context) : IOrderRepository
    {
        readonly ApplicationDbContext _context = context;
        public async Task<Order?> CreateAsync(string userId, int addressId, string? code, string paymentMethod, string? transactionId)
        {
            var cartItems = await _context.Carts.Include(c => c.Product).Where(c => c.UserId == userId).ToListAsync();
            if (cartItems.Count == 0)
            {
                return null;
            }

            double totalAmount = cartItems.Sum(item => item.Product.Price * item.Quantity);



            var coupon = code != null ? await _context.Coupons.FirstOrDefaultAsync(x => x.Code == code) : null;
            var order = new Order
            {
                OrderDate = DateTime.UtcNow,
                TotalAmount = totalAmount,
                OrderStatus = "Pending",
                UserId = userId,
                AddressId = addressId,
                CouponId = coupon?.Id,
                OrderItems = cartItems.Select(c => new OrderItem
                {
                    ProductId = c.ProductId,
                    Quantity = c.Quantity,
                    Price = c.Product.Price
                }).ToList()
            };
            _context.Orders.Add(order);
            _context.Carts.RemoveRange(cartItems);
            await _context.SaveChangesAsync();
            var payment = new Payment
            {
                PaymentMethod = paymentMethod,
                AmountPaid = totalAmount,
                PaymentStatus = "Pending",
                TransactionId = transactionId,
                OrderId = order.Id
            };
            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<Order?> DeleteAsync(int id)
        {
            var orderExist = await _context.Orders.FirstOrDefaultAsync(x => x.Id == id);
            if (orderExist == null) return null;
            _context.Orders.Remove(orderExist);
            await _context.SaveChangesAsync();
            return orderExist;
        }

        public async Task<List<Order>> GetAllAsync(string userId)
        {
            return await _context.Orders.Where(x => x.UserId == userId).ToListAsync();
        }
    }
}