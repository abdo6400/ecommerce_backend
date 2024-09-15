using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Interfaces;
using Newtonsoft.Json;
using Stripe.Checkout;

namespace api.Services
{
    public class StripePaymentService : IPaymentService
    {
        public async Task<string> CreatePaymentSession(Dictionary<string, string> data)
        {
            // Set your domain or base URL
            var domain = "http://ama.runasp.net";
            
            // Create the session options for Stripe Checkout
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = ["card"],
                LineItems =
                [
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            Currency = "usd", // Define the currency (e.g., USD)
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = "Test Product", // The name of the product
                            },
                            UnitAmount = 5000, // The price in cents (5000 = $50.00)
                        },
                        Quantity = 1, // Set the quantity
                    },
                ],
                Metadata = data,
                Mode = "payment", // Mode can be 'payment' or 'subscription'
                SuccessUrl = $"{domain}/success?session_id={{CHECKOUT_SESSION_ID}}",
                CancelUrl = $"{domain}/cancel",
            };

            // Create the session using the Stripe SessionService
            var service = new SessionService();
            Session session = await service.CreateAsync(options);

            // Return the session URL for redirect
            return session.Url;
        }
    }
}