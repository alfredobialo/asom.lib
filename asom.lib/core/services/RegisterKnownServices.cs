using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace asom.lib.core.services
{
    public static class RegisterKnownServices
    {
        public static IServiceCollection RegisterUtilityServices(this IServiceCollection services)
        {
            services.AddSingleton<IObjectHashGenerator, ObjectHashGenerator>();
            services.TryAddScoped(typeof(ICacheManager<>), typeof(CacheManager<>));
            services.TryAddScoped(typeof(ICacheManagerFactory), typeof(CacheManagerFactory));
            services.TryAddScoped(typeof(IServiceListFilter<>), typeof(ServiceListFilter<>));
            return services;
        }
    }
}
