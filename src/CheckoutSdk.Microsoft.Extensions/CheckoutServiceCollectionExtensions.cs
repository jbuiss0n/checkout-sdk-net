using System;
using Checkout;
using Checkout.Microsoft.Extensions;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// This class adds extension methods to IServiceCollection making it easier to add the Checkout client
    /// to the NET Core dependency injection framework.
    /// </summary>
    public static class CheckoutServiceCollectionExtensions
    {
        public static IServiceCollection AddCheckoutSdk(this IServiceCollection services, CheckoutConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            services.AddSingleton<IHttpClientFactory>(new DefaultHttpClientFactory());
            services.AddSingleton<CheckoutConfiguration>(configuration);
            services.AddSingleton<IApiClient, ApiClient>();

            return services;
        }

        public static IServiceCollection AddCheckoutSdk(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));
            
            var checkoutOptions = configuration.GetCheckoutOptions();
            return services.AddCheckoutSdk(checkoutOptions.CreateConfiguration());
        }
    }
}