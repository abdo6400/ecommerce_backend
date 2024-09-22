using api.Repositories;
using api.Services;
using api.Settings;

namespace api.Infrastructure
{
    public static class ServicesConfiguration
    {
        public static void AddServiceConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
            services.Configure<StripeSettings>(configuration.GetSection("Stripe"));

            Stripe.StripeConfiguration.ApiKey = configuration.GetSection("Stripe")["SecretKey"];

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ISubCategoryRepository, SubCategoryRepository>();
            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped<IWishlistRepository, WishlistRepository>();
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<ICouponRepository, CouponRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            // services.AddScoped<IPaymentRepository, PaymentRepository>();
            // services.AddScoped<IDeliveryRepository, DeliveryRepository>();

            services.AddSingleton<ITokenService, TokenService>();
            services.AddSingleton<IOtpService, OtpService>();
            services.AddSingleton<IFileService, FileService>();
            services.AddSingleton<IEmailSenderService, EmailSenderService>();
            services.AddSingleton<IPaymentService, StripePaymentService>();
        }
    }
}